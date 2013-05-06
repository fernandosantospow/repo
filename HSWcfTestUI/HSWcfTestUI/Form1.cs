using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.Services;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Data;


namespace HSWcfTestUI
{
    public partial class Form1 : Form
    {

        public Form1()
        {

            InitializeComponent();
        }

        HotelService.HotelService service = new HotelService.HotelService();
        DataSet resultado = new DataSet();

   
        public bool ValidaCampos()
        {
            if (cb1.Text == "" ||
                cb2.Text == "" ||
                cb3.Text == "" ||
                cbx_tipoCama.Text == "" ||
                numAdultos.Value == 0 ||
                txtEmpresaId.Text == ""||
                txtMaxHotel.Text == ""||
                txt_markupAgencia.Text == ""
                )

               
                
            
                return false;
            
            else 
                return true;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            
            int destinoId;
            cbx_QtdCrianca.SelectedIndex = 0;
            cbxIdade1.SelectedIndex = 0;
            cbxIdade2.SelectedIndex = 0;
            cbxIdade3.SelectedIndex = 0;
            cbx_tipoCama.SelectedIndex = 0;
            txtMaxHotel.Text = 0.ToString();
            txt_markupAgencia.Text = 0.ToString();
            cb1.SelectedIndex = 0;
            cb2.SelectedValue = "BR";
            cb3.SelectedValue = 100304;
            dtpCheckin.Value = DateTime.Now.AddDays(1);
            dtpCheckout.Value = dtpCheckin.Value.AddDays(4);
            txtEmpresaId.Text = 1.ToString();
            numSenior.Maximum = 3;
            txtEmpresaId.MaxLength = 5;
            txtMaxHotel.MaxLength = 5;
            txt_markupAgencia.MaxLength = 5;


            
             
             
        }

        public void button1_Click(object sender, EventArgs e)
        {
            tabControl2.SelectedTab = tabPage3;

            try
            {

                HotelService.HotelService service = new HotelService.HotelService();
                service.CookieContainer = new System.Net.CookieContainer();
                service.Login(Convert.ToInt32(txtEmpresaId.Text));


                if (ValidaCampos() == true)
                {

                    Cursor.Current = Cursors.WaitCursor;
                    lblPesquisaStatus.Text = "Buscando...";
                    lblPesquisaStatus.ForeColor = System.Drawing.Color.Black;
                    lblPesquisaStatus.Refresh();

                    //HotelService.HotelService service = new HotelService.HotelService();

                    string str = cb3.SelectedValue.ToString();
                    int destinoId = Convert.ToInt32(str);
                    HotelService.eTipoCama tipoCama;

                    switch (cbx_tipoCama.SelectedIndex)
                    {
                        case 0:
                            tipoCama = HotelService.eTipoCama.CamaSeparada;
                            break;
                        case 1:
                            tipoCama = HotelService.eTipoCama.CamaCasal;
                            break;
                        default:
                            tipoCama = HotelService.eTipoCama.NaoRelevante;
                            break;
                    }

                    HotelService.eIdioma idioma = (HotelService.eIdioma)Enum.Parse(typeof(HotelService.eIdioma), cb1.Text);

                    decimal adultosConvert = numAdultos.Value;
                    byte adultos = Convert.ToByte(adultosConvert);

                    decimal seniorConvert = numSenior.Value;
                    byte senior = Convert.ToByte(seniorConvert);

                    string criancaConvert = cbx_QtdCrianca.Text;
                    int intCriancaConvert = Convert.ToInt32(criancaConvert);
                    byte crianca = Convert.ToByte(intCriancaConvert);

                    int qtdeChild = Convert.ToInt32(cbx_QtdCrianca.Text);
                    int[] IdadeCriancas;

                    bool somenteDisponiveis = chk_somenteDisponiveis.Checked;
                    bool compararMaisBaratos = chk_compararMaisBaratos.Checked;

                    if (qtdeChild > 0)
                    {
                        IdadeCriancas = new int[qtdeChild];
                        int contador = IdadeCriancas.Length;

                        switch (contador)
                        {
                            case 1:
                                IdadeCriancas[0] = Convert.ToInt32(cbxIdade1.Text);
                                break;
                            case 2:
                                IdadeCriancas[0] = Convert.ToInt32(cbxIdade1.Text);
                                IdadeCriancas[1] = Convert.ToInt32(cbxIdade2.Text);
                                break;
                            case 3:
                                IdadeCriancas[0] = Convert.ToInt32(cbxIdade1.Text);
                                IdadeCriancas[1] = Convert.ToInt32(cbxIdade2.Text);
                                IdadeCriancas[2] = Convert.ToInt32(cbxIdade3.Text);
                                break;
                        }
                    }
                    else
                    {
                        IdadeCriancas = null;
                    }

                    HotelService.SearchInfo searchInfo = new HotelService.SearchInfo();

                    searchInfo.IsAssync = chk_asyinc.Checked;
                    searchInfo.DestinoId = destinoId;
                    searchInfo.DtCheckIn = Convert.ToDateTime(dtpCheckin.Text);
                    searchInfo.DtCheckOut = Convert.ToDateTime(dtpCheckout.Text);
                    searchInfo.SomentePreferencial = chk_soPreferencial.Checked;
                    searchInfo.MarkupAgencia = Convert.ToDecimal(txt_markupAgencia.Text);
                    searchInfo.Idioma = idioma;
                    searchInfo.ReservaOnRequest = chk_resOnReq.Checked;
                    searchInfo.MaxHotel = Convert.ToInt32(txtMaxHotel.Text);


                    searchInfo.QuartoInfo = new HotelService.cQuartoInfo[]
            
            {
            new  HotelService.cQuartoInfo()
            {
                  TipoCama = tipoCama,
                  NumAdulto = adultos,
                  NumSenior = senior,
                  NumCriancas = crianca,
                  IdadeCriancas = IdadeCriancas     
            }
            
            };



                    long hotels = service.HotelSearch(searchInfo);
                    lblTeste.Text = hotels.ToString();
                    lblTrackid2.Text = hotels.ToString();

                    resultado = new DataSet();

                    resultado = service.getResultadoHoteisUnitedMelhor(hotels, -1, -1, somenteDisponiveis, compararMaisBaratos, null);

                    dataGridView1.Columns.Clear();
                    dataGridView2.Columns.Clear();
                    dataGridView3.Columns.Clear();

                    dataGridView1.DataSource = resultado.Tables[0];
                    dataGridView2.DataSource = resultado.Tables[1];
                    dataGridView3.DataSource = resultado.Tables[2];

                    if (resultado.Tables[1].Rows.Count > 0)
                    {
                        lblPesquisaStatus.Text = "Busca Finalizada!";
                        lblPesquisaStatus.ForeColor = System.Drawing.Color.Black;
                        lblPesquisaStatus.Refresh();
                        Cursor.Current = Cursors.Default;
                    }
                    else
                    {
                        lblPesquisaStatus.Text = "Não retornou resultado";
                        lblPesquisaStatus.ForeColor = System.Drawing.Color.Red;
                    }


                    //Habilita o botão de tarifação, caso o datagridview2 tenha retornado algum valor
                    if (dataGridView2.RowCount > 0 && dataGridView2.Rows[0].Cells[0].Value != null)
                    {
                        btnTarifar.Enabled = true;
                    }
                    else
                    {
                        btnTarifar.Enabled = false;
                    }


                }
                else
                {
                    MessageBox.Show("Todo os valores devem ser preenchidos");
                }

            }

            catch(Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }




        }

        private void cb1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (HotelService.HotelService service = new HotelService.HotelService())
            {
                try
                {
                    HotelService.eIdioma idioma = (HotelService.eIdioma)Enum.Parse(typeof(HotelService.eIdioma), cb1.Text);
                    service.CookieContainer = new System.Net.CookieContainer();
                    service.Login(Convert.ToInt32(1));

                    DataSet dsPaises = service.PesquisaDestinoPais(idioma);
                   
                        if (dsPaises != null)
                        {
                            cb2.DataSource = dsPaises.Tables[0];
                            cb2.DisplayMember = "Pais";
                            cb2.ValueMember = "IdPais";
                        }
                        if (cb3.Text != "")
                        {
                            cb3.Text = "";
                        }  
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

           }

       }

        private void cb2_SelectedIndexChanged(object sender, EventArgs e)
        {
            HotelService.HotelService service = new HotelService.HotelService();
            HotelService.eIdioma idioma = (HotelService.eIdioma)Enum.Parse(typeof(HotelService.eIdioma), cb1.Text);
            string pais = cb2.SelectedValue.ToString();

            service.CookieContainer = new System.Net.CookieContainer();
            service.Login(Convert.ToInt32(1));

            DataSet dsCidades = service.PesquisaDestinoPorPais(pais, idioma);

                if (dsCidades != null)
                {
                    cb3.DataSource = dsCidades.Tables[0];
                    cb3.DisplayMember = "Nome";
                    cb3.ValueMember = "IdCidade";
                }

                if (cb3.Text != "")
                {
                    cb3.Text = "";
                }
        }

        private void cb3_SelectedIndexChanged(object sender, EventArgs e)
        {
            HotelService.HotelService service = new HotelService.HotelService();

            string str = cb3.SelectedValue.ToString();

        }

        private void cbx_QtdCrianca_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iCrianca = int.Parse(cbx_QtdCrianca.Text);

            if (cbx_QtdCrianca.Text != "")
            {
                int ncrianca = int.Parse(cbx_QtdCrianca.Text); 
            }
            

            switch (iCrianca)
            {
                case 0:
                    cbxIdade1.Visible = false;
                    cbxIdade2.Visible = false;
                    cbxIdade3.Visible = false;
                    lblIdadeCriancas.Visible = false;
                    break;
                case 1:
                    cbxIdade1.Visible = true;
                    cbxIdade2.Visible = false;
                    cbxIdade3.Visible = false;
                    lblIdadeCriancas.Visible = true;
                    break;
                case 2:
                    cbxIdade1.Visible = true;
                    cbxIdade2.Visible = true;
                    cbxIdade3.Visible = false;
                    lblIdadeCriancas.Visible = true;
                    break;
                case 3:
                    cbxIdade1.Visible = true;
                    cbxIdade2.Visible = true;
                    cbxIdade3.Visible = true;
                    lblIdadeCriancas.Visible = true;
                    break;
            }
            
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            string cell = dataGridView2.Rows[0].Cells[0].Value.ToString();

            if (cell == "")
            {
                  lblPesquisaStatus.Text = "Nenhum valor selecionado";
            //    timer1.Interval = 2000;
            //    lblPesquisaStatus.Text = "";
            }
            else
            {
                int index = e.RowIndex;
                DataGridViewRow row = dataGridView2.Rows[index];
                string idHotel = row.Cells[1].Value.ToString();
                DataRow[] dr = resultado.Tables[2].Select("HotelId=" + idHotel);
                dataGridView3.DataSource = dr.CopyToDataTable();
            }
        }

        private void dtpCheckin_ValueChanged(object sender, EventArgs e)
        {
            if (dtpCheckin.Value < DateTime.Now)
            {
                MessageBox.Show("Data de check-in não pode ser anterior a data atual");
                dtpCheckin.Value = DateTime.Now;
            }
            dtpCheckout.Value = dtpCheckin.Value.AddDays(1);
        }

        private void dtpCheckout_ValueChanged(object sender, EventArgs e)
        {
            if (dtpCheckout.Value < dtpCheckin.Value)
            {
                dtpCheckout.Value = dtpCheckin.Value;
            }
        }
        private void btnBuscaTrackId_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;


            if (txtBuscaTrackId.TextLength == 9)
            {

                Cursor.Current = Cursors.WaitCursor;
                lblPesquisaTrack.Text = "Buscando...";
                lblPesquisaTrack.ForeColor = System.Drawing.Color.Black;
                lblPesquisaTrack.Refresh();
                tabControl2.SelectedTab = tabPage3;
                dataGridView1.Columns.Clear();
                dataGridView2.Columns.Clear();
                dataGridView3.Columns.Clear();
                dgvPesquisaTrack.Columns.Clear();
                dgvInfoQuartos.Columns.Clear();
                dgvHotel.Columns.Clear();
                dgvFacilidade.Columns.Clear();
                dgvImagem.Columns.Clear();
                dgvTipoQuarto.Columns.Clear();
                dgvDispPeriodo.Columns.Clear();
                dgvRegime.Columns.Clear();
                dgvRegimeSuplementos.Columns.Clear();


                HotelService.HotelService service = new HotelService.HotelService();
                service.CookieContainer = new System.Net.CookieContainer();
                service.Login(Convert.ToInt32(1));

                DataSet resultadoTrackId = new DataSet();

                long resultadoTrack = Int64.Parse(txtBuscaTrackId.Text);
                resultadoTrackId = service.getResultadoHoteisUnitedMelhor(resultadoTrack, -1, -1, false, false, null);


                dataGridView1.DataSource = resultadoTrackId.Tables[0];

                if (resultadoTrackId.Tables[0].Rows.Count > 0)
                {
                  lblPesquisaTrack.Text = "Pesquisa Finalizada";
                    
                }
                else
                {
                    lblPesquisaTrack.Text = "Pesquisa não retornou resultados";  
                    
                }

                btnTarifar.Enabled = false;
                lblTrackid2.Text = "";

            }
            else 
            {
                MessageBox.Show("TrackId Incorreto");
            }

            

        }

        private void btnTarifar_Click(object sender, EventArgs e)
        {
                Cursor.Current = Cursors.WaitCursor;
                lblPesquisaStatus.Text = "Tarifando...";
                lblPesquisaStatus.ForeColor = System.Drawing.Color.Black;
                lblPesquisaStatus.Refresh();

            if (dataGridView2.RowCount != 0)
            {
                service.CookieContainer = new System.Net.CookieContainer();
                service.Login(Convert.ToInt32(1));

                int index = dataGridView2.CurrentCell.RowIndex;
                DataGridViewRow row = dataGridView2.Rows[index];
                long TrackId = Convert.ToInt64(row.Cells[0].Value);
                int HotelId = Convert.ToInt32(row.Cells[1].Value);
                int RegimeId = Convert.ToInt32(row.Cells[19].Value);
           
                HotelService.ValuationData data = new HotelService.ValuationData()
                    {
                        TrackId = TrackId,
                        HotelId = HotelId,
                        RegimeId = RegimeId
                    };

                DataSet tarifacao = new DataSet();
                tarifacao = service.MakeValuation(data);

                dgvPesquisaTrack.DataSource = tarifacao.Tables[0];
                dgvInfoQuartos.DataSource = tarifacao.Tables[1];
                dgvHotel.DataSource = tarifacao.Tables[2];
                dgvFacilidade.DataSource = tarifacao.Tables[3];
                dgvImagem.DataSource = tarifacao.Tables[4];
                dgvTipoQuarto.DataSource = tarifacao.Tables[5];
                dgvDispPeriodo.DataSource = tarifacao.Tables[6];
                dgvRegime.DataSource = tarifacao.Tables[7];
                dgvRegimeSuplementos.DataSource = tarifacao.Tables[8];
                lblPesquisaStatus.Text = "Tarifação concluída";
                lblPesquisaStatus.Refresh();
                tabControl2.SelectedTab = tabPage4;
            }

            else
            {
                MessageBox.Show("Nenhum valor selecionado");
            }

        }

    

    }
}
