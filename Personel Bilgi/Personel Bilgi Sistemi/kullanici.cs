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
    public partial class kullanici : Form
    {
        public kullanici()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=HARUN-NB;Initial Catalog=personel_alfa;Integrated Security=True;");
        System.Data.DataTable tablo = new System.Data.DataTable();


        yonetici yntc = new yonetici();


        // Giriş butonuna tıklama olayı
        private void grs_yon_btn_Click(object sender, EventArgs e)
        {
            // Veritabanı bağlantı dizesi
            string connectionString = "Data Source=HARUN-NB;Initial Catalog=personel_alfa;Integrated Security=True";

            // Veritabanı bağlantısı açılıyor
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Kullanıcı adını ve şifresini veritabanından çekmek için sorgu
                    string query = "SELECT Password FROM Yonetici_Giris WHERE Username = @Username";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Parametreli sorgu kullanıyoruz (SQL enjeksiyonundan korunmak için)
                        cmd.Parameters.AddWithValue("@Username", yon_txt.Text);

                        // Veritabanından şifreyi çekiyoruz
                        string passwordFromDb = cmd.ExecuteScalar()?.ToString();

                        // Şifre doğrulaması yapılıyor
                        if (passwordFromDb == sifre_yon_txt.Text)
                        {
                            // Doğruysa formu göster
                            yntc.Show();
                            temizleGiris();
                            // Formu gizle ve oturum kapatma işlemini yap
                            this.Hide(); // Mevcut formu gizle

                            
                            

                            
                        }
                        else
                        {
                            // Yanlış şifre veya kullanıcı adı
                            MessageBox.Show("Yanlış Şifre Veya Kullanıcı Adı");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Olası hataları yakalamak için exception handling
                    MessageBox.Show("Veritabanı bağlantı hatası: " + ex.Message);
                }
            }
        }




        void temizleGiris()
        {

            yon_txt.Text = "";
            sifre_yon_txt.Text = "";

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

        private void kullanici_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'personel_alfaDataSet5.alfa_personel' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.alfa_personelTableAdapter2.Fill(this.personel_alfaDataSet5.alfa_personel);
            
          

            //ComboBox verilerini sql den alır
            SqlCommand komut1 = new SqlCommand("SELECT * FROM BOLUM", baglanti);
            SqlDataReader dr1;
            baglanti.Open();
            dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                blm_fltr_cbox.Items.Add(dr1["Bolum"]);

            }
            baglanti.Close();

            SqlCommand komut2 = new SqlCommand("SELECT * FROM BIRIM", baglanti);
            SqlDataReader dr2;
            baglanti.Open();
            dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                brm_fltr_cbox.Items.Add(dr2["Bırım"]);

            }
            baglanti.Close();


            SqlCommand komut3 = new SqlCommand("SELECT * FROM FIRMA", baglanti);
            SqlDataReader dr3;
            baglanti.Open();
            dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                firma_fltr_cbox.Items.Add(dr3["Fırma"]);

            }
            baglanti.Close();

            SqlCommand komut4 = new SqlCommand("SELECT * FROM GRUP", baglanti);
            SqlDataReader dr4;
            baglanti.Open();
            dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                grup_fltr_cbox.Items.Add(dr4["Grup"]);

            }
            baglanti.Close();


            SqlCommand komut5 = new SqlCommand("SELECT * FROM GOREV", baglanti);
            SqlDataReader dr5;
            baglanti.Open();
            dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                görev_fltr_cbox.Items.Add(dr5["Gorev"]);
            }
            baglanti.Close();
        }

        private void excel_çıktı_btn_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application çıktı = new Microsoft.Office.Interop.Excel.Application();
            çıktı.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook kitap = çıktı.Workbooks.Add(System.Reflection.Missing.Value);
            Microsoft.Office.Interop.Excel.Worksheet sayfa = (Microsoft.Office.Interop.Excel.Worksheet)kitap.Sheets[1];
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)sayfa.Cells[1, i + 1];
                range.Value2 = dataGridView1.Columns[i].HeaderText;
            }
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)sayfa.Cells[j + 2, i + 1];
                    range.Value2 = dataGridView1[i, j].Value;
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
                soyad_fltr_cbox.Paste();
            }
        }

        private void sicil_fltr_cbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                // Kopyalanan metni yapıştır
                sicil_fltr_cbox.Paste();
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
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void ad_fltr_cbox_TextChanged(object sender, EventArgs e)
        {
            adFilter = "Personel_Adı like '%" + ad_fltr_cbox.Text + "%'";
            ApplyFilters();
        }

        

        private void soyad_fltr_cbox_TextChanged(object sender, EventArgs e)
        {
            soyadFilter = "Personel_Soyadı like '%" + soyad_fltr_cbox.Text + "%'";
            ApplyFilters();
        }

        private void sicil_fltr_cbox_TextChanged(object sender, EventArgs e)
        {
            sicilFilter = "Sicil_No like '%" + sicil_fltr_cbox.Text + "%'";
            ApplyFilters();
        }

        private void blm_fltr_cbox_TextChanged(object sender, EventArgs e)
        {
            blmFilter = "Bolum like '%" + blm_fltr_cbox.Text + "%'";
            ApplyFilters();
        }

        private void brm_fltr_cbox_TextChanged(object sender, EventArgs e)
        {
            brmFilter = "Bırım like '%" + brm_fltr_cbox.Text + "%'";
            ApplyFilters();
        }

        private void firma_fltr_cbox_TextChanged(object sender, EventArgs e)
        {
            firmaFilter = "Fırma like '%" + firma_fltr_cbox.Text + "%'";
            ApplyFilters();
        }

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
    }

}
