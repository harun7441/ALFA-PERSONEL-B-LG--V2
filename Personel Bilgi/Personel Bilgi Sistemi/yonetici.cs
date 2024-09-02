using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personel_Bilgi_Sistemi
{
    public partial class yonetici : Form
    {
        public yonetici()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=HARUN-NB;Initial Catalog=personel_alfa;Integrated Security=True;");
        System.Data.DataTable tablo = new System.Data.DataTable();


        private void yonetici_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'personel_alfaDataSet8.alfa_personel' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.alfa_personelTableAdapter3.Fill(this.personel_alfaDataSet8.alfa_personel);
           
            
            //ComboBox verilerini sql den alır
            SqlCommand komut1 = new SqlCommand("SELECT * FROM BOLUM", baglanti);
            SqlDataReader dr1;
            baglanti.Open();
            dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                blm_fltr_cbox.Items.Add(dr1["Bolum"]);
                bolum_txt.Items.Add(dr1["Bolum"]);

            }
            baglanti.Close();

            SqlCommand komut2 = new SqlCommand("SELECT * FROM BIRIM", baglanti);
            SqlDataReader dr2;
            baglanti.Open();
            dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                brm_fltr_cbox.Items.Add(dr2["Bırım"]);
                birim_txt.Items.Add(dr2["Bırım"]);

            }
            baglanti.Close();


            SqlCommand komut3 = new SqlCommand("SELECT * FROM FIRMA", baglanti);
            SqlDataReader dr3;
            baglanti.Open();
            dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                firma_fltr_cbox.Items.Add(dr3["Fırma"]);
                firma_txt.Items.Add(dr3["Fırma"]);

            }
            baglanti.Close();

            SqlCommand komut4 = new SqlCommand("SELECT * FROM GRUP", baglanti);
            SqlDataReader dr4;
            baglanti.Open();
            dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                grup_fltr_cbox.Items.Add(dr4["Grup"]);
                grup_txt.Items.Add(dr4["Grup"]);

            }
            baglanti.Close();


            SqlCommand komut5 = new SqlCommand("SELECT * FROM GOREV", baglanti);
            SqlDataReader dr5;
            baglanti.Open();
            dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                görev_fltr_cbox.Items.Add(dr5["Gorev"]);
                gorev_txt.Items.Add(dr5["Gorev"]);
            }
            baglanti.Close();


            SqlCommand komut6 = new SqlCommand("SELECT * FROM ACİL_KİSİ", baglanti);
            SqlDataReader dr6;
            baglanti.Open();
            dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                acil_kisi_txt.Items.Add(dr6["Acil_Kisi"]);
            }
            baglanti.Close();
        }


        void temizleFiltre()
        {

            sicil_fltr_cbox.Text = "";
            ad_fltr_cbox.Text = "";
            soyad_fltr_cbox.Text = "";
            blm_fltr_cbox.Text = "";
            brm_fltr_cbox.Text = "";
            firma_fltr_cbox.Text = "";
            grup_fltr_cbox.Text = "";
            görev_fltr_cbox.Text = "";
            ad_fltr_cbox.Focus();
        }

        private void temizle_fltr_btn_Click(object sender, EventArgs e)
        {
            temizleFiltre();
        }

        private void excel_çıktı_btn_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application çıktı = new Microsoft.Office.Interop.Excel.Application();
            çıktı.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook kitap = çıktı.Workbooks.Add(System.Reflection.Missing.Value);
            Microsoft.Office.Interop.Excel.Worksheet sayfa = (Microsoft.Office.Interop.Excel.Worksheet)kitap.Sheets[1];
            for (int i = 0; i < OTURUM_BTN.Columns.Count; i++)
            {
                Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)sayfa.Cells[1, i + 1];
                range.Value2 = OTURUM_BTN.Columns[i].HeaderText;
            }
            for (int i = 0; i < OTURUM_BTN.Columns.Count; i++)
            {
                for (int j = 0; j < OTURUM_BTN.Rows.Count; j++)
                {
                    Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)sayfa.Cells[j + 2, i + 1];
                    range.Value2 = OTURUM_BTN[i, j].Value;
                }
            }
        }

        private void ad_fltr_cbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                // Kopyalanan metni yapıştır
                ad_fltr_cbox.Paste();
            }
        }

        private void soyad_fltr_cbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                // Kopyalanan metni yapıştır
                ad_fltr_cbox.Paste();
            }
        }

        private void sicil_fltr_cbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                // Kopyalanan metni yapıştır
                ad_fltr_cbox.Paste();
            }
        }






        private void ad_fltr_cbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void soyad_fltr_cbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void sicil_fltr_cbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void blm_fltr_cbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void brm_fltr_cbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void firma_fltr_cbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void grup_fltr_cbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void görev_fltr_cbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }





        string sicilFilter = "", adFilter = "", soyadFilter = "", blmFilter = "", brmFilter = "", firmaFilter = "", grupFilter = "", görevFilter = "";

        private void grup_fltr_cbox_TextChanged(object sender, EventArgs e)
        {
            grupFilter = "Grup like '%" + grup_fltr_cbox.Text + "%'";
            ApplyFilters();
        }

        private void görev_fltr_cbox_TextChanged(object sender, EventArgs e)
        {
            görevFilter = "Gorev like '%" + görev_fltr_cbox.Text + "%'";
            ApplyFilters();
        }

        private void firma_fltr_cbox_TextChanged(object sender, EventArgs e)
        {
            firmaFilter = "Fırma like '%" + firma_fltr_cbox.Text + "%'";
            ApplyFilters();
        }

        private void brm_fltr_cbox_TextChanged(object sender, EventArgs e)
        {
            brmFilter = "Bırım like '%" + brm_fltr_cbox.Text + "%'";
            ApplyFilters();
        }

        

        private void blm_fltr_cbox_TextChanged(object sender, EventArgs e)
        {
            blmFilter = "Bolum like '%" + blm_fltr_cbox.Text + "%'";
            ApplyFilters();
        }

        

        private void sicil_fltr_cbox_TextChanged(object sender, EventArgs e)
        {
            sicilFilter = "Sicil_No like '%" + sicil_fltr_cbox.Text + "%'";
            ApplyFilters();
        }

        

        private void soyad_fltr_cbox_TextChanged(object sender, EventArgs e)
        {
            soyadFilter = "Personel_Soyadı like '%" + soyad_fltr_cbox.Text + "%'";
            ApplyFilters();
        }

        

        private void ad_fltr_cbox_TextChanged(object sender, EventArgs e)
        {
            adFilter = "Personel_Adı like '%" + ad_fltr_cbox.Text + "%'";
            ApplyFilters();
        }




        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            // SEÇİLEN KİŞİYİ EKRANDA GÖSTERİR.
            int secim = OTURUM_BTN.SelectedCells[0].RowIndex;
            personel_txt.Text = OTURUM_BTN.Rows[secim].Cells[0].Value.ToString();
            sicil_txt.Text = OTURUM_BTN.Rows[secim].Cells[1].Value.ToString();
            ad_txt.Text = OTURUM_BTN.Rows[secim].Cells[2].Value.ToString();
            soyad_txt.Text = OTURUM_BTN.Rows[secim].Cells[3].Value.ToString();
            tc_txt.Text = OTURUM_BTN.Rows[secim].Cells[4].Value.ToString();
            tel_txt.Text = OTURUM_BTN.Rows[secim].Cells[5].Value.ToString();
            dahili_no_txt.Text = OTURUM_BTN.Rows[secim].Cells[6].Value.ToString();
            iş_no_txt.Text = OTURUM_BTN.Rows[secim].Cells[7].Value.ToString();
            acil_kisi_txt.Text = OTURUM_BTN.Rows[secim].Cells[8].Value.ToString();
            acil_kisi__bil_txt.Text = OTURUM_BTN.Rows[secim].Cells[9].Value.ToString();


            acil_tel_txt.Text = OTURUM_BTN.Rows[secim].Cells[10].Value.ToString();
            bolum_txt.Text = OTURUM_BTN.Rows[secim].Cells[11].Value.ToString();
            birim_txt.Text = OTURUM_BTN.Rows[secim].Cells[12].Value.ToString();
            firma_txt.Text = OTURUM_BTN.Rows[secim].Cells[13].Value.ToString();
            grup_txt.Text = OTURUM_BTN.Rows[secim].Cells[14].Value.ToString();
            gorev_txt.Text = OTURUM_BTN.Rows[secim].Cells[15].Value.ToString();

           
        }
        

        private void sicil_txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                // Kopyalanan metni yapıştır
                sicil_txt.Paste();
            }
        }

        private void ad_txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                // Kopyalanan metni yapıştır
                ad_txt.Paste();
            }
        }

        private void soyad_txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                // Kopyalanan metni yapıştır
                soyad_txt.Paste();
            }
        }

        private void tc_txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                // Kopyalanan metni yapıştır
                tc_txt.Paste();
            }
        }

        private void tel_txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                // Kopyalanan metni yapıştır
                tel_txt.Paste();
            }
        }

        private void acil_tel_txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                // Kopyalanan metni yapıştır
                acil_tel_txt.Paste();
            }
        }

        private void dahili_no_txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                // Kopyalanan metni yapıştır
                acil_tel_txt.Paste();
            }
        }

        private void iş_no_txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                // Kopyalanan metni yapıştır
                acil_tel_txt.Paste();
            }
        }

        private void acil_kisi__bil_txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                // Kopyalanan metni yapıştır
                acil_tel_txt.Paste();
            }
        }

        private void ApplyFilters()
        {
            List<string> filters = new List<string>();

            if (!string.IsNullOrEmpty(sicilFilter)) filters.Add(sicilFilter);
            if (!string.IsNullOrEmpty(adFilter)) filters.Add(adFilter);
            if (!string.IsNullOrEmpty(soyadFilter)) filters.Add(soyadFilter);
            if (!string.IsNullOrEmpty(blmFilter)) filters.Add(blmFilter);
            if (!string.IsNullOrEmpty(brmFilter)) filters.Add(brmFilter);
            if (!string.IsNullOrEmpty(firmaFilter)) filters.Add(firmaFilter);
            if (!string.IsNullOrEmpty(grupFilter)) filters.Add(grupFilter);
            if (!string.IsNullOrEmpty(görevFilter)) filters.Add(görevFilter);

            string query = "select * from alfa_personel";

            if (filters.Count > 0)
            {
                query += " where " + string.Join(" AND ", filters);
            }

            tablo.Clear();
            baglanti.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, baglanti);
            dataAdapter.Fill(tablo);
            OTURUM_BTN.DataSource = tablo;
            baglanti.Close();
        }





        // *********************  Veri eklme tarafı  *********************

        void temizle()
        {
            sicil_txt.Text = "";
            personel_txt.Text = "";
            ad_txt.Text = "";
            soyad_txt.Text = "";
            tc_txt.Text = "";
            tel_txt.Text = "";
            acil_tel_txt.Text = "";
            bolum_txt.Text = "";
            birim_txt.Text = "";
            firma_txt.Text = "";
            grup_txt.Text = "";
            gorev_txt.Text = "";
            dahili_no_txt.Text = "";
            iş_no_txt.Text = "";
            acil_kisi_txt.Text = "";
            acil_kisi__bil_txt.Text = "";
 
            sicil_txt.Focus();

        }

        

        // *********************  DataGridView'i yenileme fonksiyonu  *********************
        private void VerileriYenile()
        {
            tablo.Clear(); // Eski veriyi temizle
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM alfa_personel", baglanti);
            da.Fill(tablo);
            OTURUM_BTN.DataSource = tablo; // DataGridView'e veriyi ata
        }





        private void kaydet_btn_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO alfa_personel (Sicil_No, Personel_Adı, Personel_Soyadı, TC, Telefon, Acil_Tel, Bolum, Bırım, Fırma, Grup, Gorev, Dahili_Numara, Is_Telefonu, Acil_Kisi, Acil_Kisi_Ad_Soyad ) VALUES (@p2, @p3, @p4, @p5, @p6, @p7, @p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16)", baglanti);
            komut.Parameters.AddWithValue("@p2", sicil_txt.Text);
            komut.Parameters.AddWithValue("@p3", ad_txt.Text);
            komut.Parameters.AddWithValue("@p4", soyad_txt.Text);
            komut.Parameters.AddWithValue("@p5", tc_txt.Text);
            komut.Parameters.AddWithValue("@p6", tel_txt.Text);
            komut.Parameters.AddWithValue("@p7", acil_tel_txt.Text);
            komut.Parameters.AddWithValue("@p8", bolum_txt.Text);
            komut.Parameters.AddWithValue("@p9", birim_txt.Text);
            komut.Parameters.AddWithValue("@p10", firma_txt.Text);
            komut.Parameters.AddWithValue("@p11", grup_txt.Text);
            komut.Parameters.AddWithValue("@p12", gorev_txt.Text);
            komut.Parameters.AddWithValue("@p13", dahili_no_txt.Text);
            komut.Parameters.AddWithValue("@p14", iş_no_txt.Text);
            komut.Parameters.AddWithValue("@p15", acil_kisi_txt.Text);
            komut.Parameters.AddWithValue("@p16", acil_kisi__bil_txt.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Personel Eklendi.");
            VerileriYenile(); // Kayıttan sonra DataGridView'i yenile
            temizle();
        }


        private void guncelle_btn_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand güncelle = new SqlCommand("UPDATE alfa_personel SET Sicil_No=@p1, Personel_Adı=@p2, Personel_Soyadı=@p3, TC=@p4, Telefon=@p5, Acil_Tel=@p6, Bolum=@p7, Bırım=@p9, Fırma=@p10, Grup=@p11, Gorev=@p12, Dahili_Numara=@p13, Is_Telefonu=@p14, Acil_Kisi=@p15, Acil_Kisi_Ad_Soyad=@p16 WHERE ID=@p8", baglanti);
            güncelle.Parameters.AddWithValue("@p1", sicil_txt.Text);
            güncelle.Parameters.AddWithValue("@p2", ad_txt.Text);
            güncelle.Parameters.AddWithValue("@p3", soyad_txt.Text);
            güncelle.Parameters.AddWithValue("@p4", tc_txt.Text);
            güncelle.Parameters.AddWithValue("@p5", tel_txt.Text);
            güncelle.Parameters.AddWithValue("@p6", acil_tel_txt.Text);
            güncelle.Parameters.AddWithValue("@p7", bolum_txt.Text);
            güncelle.Parameters.AddWithValue("@p8", personel_txt.Text);
            güncelle.Parameters.AddWithValue("@p9", birim_txt.Text);
            güncelle.Parameters.AddWithValue("@p10", firma_txt.Text);
            güncelle.Parameters.AddWithValue("@p11", grup_txt.Text);
            güncelle.Parameters.AddWithValue("@p12", gorev_txt.Text);
            güncelle.Parameters.AddWithValue("@p13", dahili_no_txt.Text);
            güncelle.Parameters.AddWithValue("@p14", iş_no_txt.Text);
            güncelle.Parameters.AddWithValue("@p15", acil_kisi_txt.Text);
            güncelle.Parameters.AddWithValue("@p16", acil_kisi__bil_txt.Text);
            güncelle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Personel Güncellemesi Tamamlandı.");
            VerileriYenile(); // Güncellemeden sonra DataGridView'i yenile
            temizle();
        }

        private void sil_btn_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand sil = new SqlCommand("DELETE FROM alfa_personel WHERE ID=@p1", baglanti);
            sil.Parameters.AddWithValue("@p1", personel_txt.Text);
            sil.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Personel Silindi.");
            VerileriYenile(); // Silmeden sonra DataGridView'i yenile
        }


        private void temizle_btn_Click(object sender, EventArgs e)
        {
            temizle();
        }







        
      





        private void sicil_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void ad_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Oturumu kapatma işlemi
            this.Hide(); // Mevcut formu gizle
                         // Giriş formunu tekrar göster
            kullanici loginForm = new kullanici(); // LoginForm, giriş formunuzun adı
            loginForm.Show();
        }

        private void soyad_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void tc_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void tel_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void dahili_no_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void iş_no_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void acil_kisi__bil_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void acil_tel_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }







    }
}
