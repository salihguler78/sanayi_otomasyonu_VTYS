using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sanayi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432; user ID=postgres; Database=dbproje; password=salih123");
               
        private void Form1_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from il",baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DisplayMember = "iladi";
            comboBox1.ValueMember = "ilkodu";
            comboBox1.DataSource = dt;
         

            NpgsqlDataAdapter da2 = new NpgsqlDataAdapter("select * from sigorta", baglanti);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            comboBox2.DisplayMember = "sigortaadi";
            comboBox2.ValueMember = "sigortaid";
            comboBox2.DataSource = dt2;

            NpgsqlDataAdapter da3 = new NpgsqlDataAdapter("select * from sanayi", baglanti);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);
            comboBox4.DisplayMember = "sanayiadi";
            comboBox4.ValueMember = "sanayiid";
            comboBox4.DataSource = dt3;

            NpgsqlDataAdapter da4 = new NpgsqlDataAdapter("select * from il", baglanti);
            DataTable dt4 = new DataTable();
            da4.Fill(dt4);
            comboBox3.DisplayMember = "iladi";
            comboBox3.ValueMember = "ilkodu";
            comboBox3.DataSource = dt4;

            NpgsqlDataAdapter da5 = new NpgsqlDataAdapter("select * from dukkan", baglanti);
            DataTable dt5 = new DataTable();
            da5.Fill(dt5);
            comboBox5.DisplayMember = "dukkanadi";
            comboBox5.ValueMember = "dukkanid";
            comboBox5.DataSource = dt5;

           



            baglanti.Close();




        }
        

        private void button1_Click(object sender, EventArgs e)   //listele
        {
            baglanti.Open();
            string sorgu = "select * from sanayi";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
                            
            baglanti.Close();
        }

        private void sanayiekle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into sanayi values(@p1,@p2,@p3)", baglanti);
            komut.Parameters.AddWithValue("@p1",int.Parse( textBox1.Text));
            komut.Parameters.AddWithValue("@p2", textBox2.Text);
            komut.Parameters.AddWithValue("@p3", int.Parse(comboBox3.SelectedValue.ToString()));
            

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kaydı başarıyla oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void sanayisil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("delete from sanayi where sanayiid=@p1;", baglanti); ;
            komut.Parameters.AddWithValue("@p1", int.Parse(textBox1.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show(" Silme işlemi gerçekleşti.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void sanayigüncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("update sanayi set sanayiadi=@p1,sanayiil=@p3 where sanayiid = @p2", baglanti);


            komut.Parameters.AddWithValue("@p1", textBox2.Text);
            komut.Parameters.AddWithValue("@p2", int.Parse( textBox1.Text));
            komut.Parameters.AddWithValue("@p3", int.Parse( comboBox3.SelectedValue.ToString()));
            komut.ExecuteNonQuery();
            MessageBox.Show(" güncelleme işlemi başarılı oldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e) //sanayi ara
        {
            baglanti.Open();
            string sorgu = "select * from sanayi where sanayiadi LIKE '%" + textBox2.Text + "%'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void dukkanlistele_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string sorgu = "select dukkanid,dukkanadi,sanayidukkan,dukkansigorta from dukkan";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];

            
            baglanti.Close();
        }

        private void dukkanekle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into dukkan values(@p1,@p2,@p3,@p4)", baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(textBox8.Text));
            komut.Parameters.AddWithValue("@p2", textBox7.Text);
            komut.Parameters.AddWithValue("@p3", int.Parse(comboBox4.SelectedValue.ToString()));
            komut.Parameters.AddWithValue("@p4", int.Parse(comboBox2.SelectedValue.ToString()));
            
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show(" kaydı başarıyla oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dukkansil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("delete from dukkan where dukkanid=@p1;", baglanti); ;
            komut.Parameters.AddWithValue("@p1", int.Parse(textBox8.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show(" Silme işlemi gerçekleşti.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dukkanguncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("update dukkan set dukkanadi=@p1,sanayidukkan=@p3,dukkansigorta=@p4 where dukkanid = @p2", baglanti);


            komut.Parameters.AddWithValue("@p1", textBox7.Text);
            komut.Parameters.AddWithValue("@p2", int.Parse(textBox8.Text));
            komut.Parameters.AddWithValue("@p3", int.Parse(comboBox4.SelectedValue.ToString()));
            komut.Parameters.AddWithValue("@p4", int.Parse(comboBox2.SelectedValue.ToString()));
            komut.ExecuteNonQuery();
            MessageBox.Show(" güncelleme işlemi başarılı oldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            baglanti.Close();
        }

        private void dukkanara_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string sorgu = "select dukkanid,dukkanadi,sanayidukkan,dukkansigorta from dukkan where dukkanadi LIKE '%" + textBox7.Text + "%'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void personellistele_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string sorgu = "select * from personel";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void personelekle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into personel values(@p1,@p2,@p3,@p4)", baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(textBox12.Text));
            komut.Parameters.AddWithValue("@p2", textBox11.Text);
            komut.Parameters.AddWithValue("@p3", textBox10.Text);
            komut.Parameters.AddWithValue("@p4", int.Parse(textBox9.Text));

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt başarıyla oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void personelsil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("delete from personel where personelid=@p1;", baglanti); ;
            komut.Parameters.AddWithValue("@p1", int.Parse(textBox12.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show(" Silme işlemi gerçekleşti.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void personelguncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("update personel set personelad=@p1,personelsoyad=@p3,personelyas=@p4 where personelid = @p2", baglanti);
            komut.Parameters.AddWithValue("@p1", textBox11.Text);
            komut.Parameters.AddWithValue("@p2", int.Parse(textBox12.Text));
            komut.Parameters.AddWithValue("@p3", textBox10.Text);
            komut.Parameters.AddWithValue("@p4", int.Parse(textBox9.Text));
            komut.ExecuteNonQuery();
            MessageBox.Show(" güncelleme işlemi başarılı oldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            baglanti.Close();
        }

        private void personelara_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string sorgu = "select * from personel where personelad LIKE '%" + textBox11.Text + "%'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void musterilistele_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string sorgu = "select * from musteri";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView4.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void musteriekle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into musteri values(@p1,@p2,@p3,@p4)", baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(textBox16.Text));
            komut.Parameters.AddWithValue("@p2", textBox15.Text);
            komut.Parameters.AddWithValue("@p3", textBox14.Text);
            komut.Parameters.AddWithValue("@p4", int.Parse(comboBox5.SelectedValue.ToString()));
            
            

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt başarıyla oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        

    }

        private void musterisil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("delete from musteri where musteriid=@p1;", baglanti); ;
            komut.Parameters.AddWithValue("@p1", int.Parse(textBox16.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show(" Silme işlemi gerçekleşti.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void musteriguncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("update musteri set musteriad=@p1,musterisoyad=@p3,tercihdukkan=@p4 where musteriid = @p2", baglanti);
            komut.Parameters.AddWithValue("@p1", textBox15.Text);
            komut.Parameters.AddWithValue("@p2", int.Parse(textBox16.Text));
            komut.Parameters.AddWithValue("@p3", textBox14.Text);
            komut.Parameters.AddWithValue("@p4", int.Parse(comboBox5.SelectedValue.ToString()));

            komut.ExecuteNonQuery();
            MessageBox.Show(" Güncelleme işlemi başarılı oldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            baglanti.Close();
        }

        private void musteriara_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string sorgu = "select * from musteri where musteriad LIKE '%" + textBox15.Text + "%'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView4.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void isalanilistele_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string sorgu = "select * from isalani";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView5.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void isalaniekle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into isalani values(@p1,@p2)", baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(textBox20.Text));
            komut.Parameters.AddWithValue("@p2", textBox19.Text);

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt başarıyla oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void isalanisil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("delete from isalani where isalaniid=@p1;", baglanti); ;
            komut.Parameters.AddWithValue("@p1", int.Parse(textBox20.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show(" Silme işlemi gerçekleşti.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void isalaniguncelle_Click(object sender, EventArgs e)
        {

            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("update isalani set isalaniadi=@p1 where isalaniid = @p2", baglanti);
            komut.Parameters.AddWithValue("@p1", textBox19.Text);
            komut.Parameters.AddWithValue("@p2", int.Parse(textBox20.Text));
          

            komut.ExecuteNonQuery();
            MessageBox.Show(" Güncelleme işlemi başarılı oldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            baglanti.Close();
        }

        private void button3_Click(object sender, EventArgs e)  // iş alani ara
        {
            baglanti.Open();
            string sorgu = "select * from isalani where isalaniadi LIKE '%" + textBox19.Text + "%'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView5.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void malzemelistele_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string sorgu = "select * from malzeme";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView6.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void malzemeekle_Click(object sender, EventArgs e)
        {

            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into malzeme values(@p1,@p2,@p3)", baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(textBox24.Text));
            komut.Parameters.AddWithValue("@p2", textBox23.Text);
            komut.Parameters.AddWithValue("@p3", int.Parse( textBox22.Text));
           


            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt başarıyla oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void malzemesil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("delete from malzeme where malzemeid=@p1;", baglanti); ;
            komut.Parameters.AddWithValue("@p1", int.Parse(textBox24.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show(" Silme işlemi gerçekleşti.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void malzemeguncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("update malzeme set malzemeadi=@p1,malzemefiyatı=@p3 where malzemeid = @p2", baglanti);
            komut.Parameters.AddWithValue("@p1", textBox23.Text);
            komut.Parameters.AddWithValue("@p2", int.Parse(textBox24.Text));
            komut.Parameters.AddWithValue("@p3", int.Parse(textBox22.Text));
            

            komut.ExecuteNonQuery();
            MessageBox.Show(" Güncelleme işlemi başarılı oldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            baglanti.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string sorgu = "select * from malzeme where malzemeadi LIKE '%" + textBox23.Text + "%'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView6.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void Tedarikcilistele_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string sorgu = "select * from tedarikci";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView7.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void tedarikciekle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into tedarikci values(@p1,@p2)", baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(textBox28.Text));
            komut.Parameters.AddWithValue("@p2", textBox27.Text);
           
            


            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt başarıyla oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void tedarikcisil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("delete from tedarikci where tedarikciid=@p1;", baglanti); ;
            komut.Parameters.AddWithValue("@p1", int.Parse(textBox28.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show(" Silme işlemi gerçekleşti.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void tedarikciguncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("update tedarikci set tedarikciadi=@p1 where tedarikciid = @p2", baglanti);
            komut.Parameters.AddWithValue("@p1", textBox27.Text);
            komut.Parameters.AddWithValue("@p2", int.Parse(textBox28.Text));
           
            

            komut.ExecuteNonQuery();
            MessageBox.Show(" Güncelleme işlemi başarılı oldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            baglanti.Close();
        }

        private void button5_Click(object sender, EventArgs e)  //tedarikci ara
        {
            baglanti.Open();
            string sorgu = "select * from tedarikci where tedarikciadi LIKE '%" + textBox27.Text + "%'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView7.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void dataGridView9_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void illistele_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string sorgu = "select * from il";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView9.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void ilekle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into il values(@p1,@p2)", baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(textBox36.Text));
            komut.Parameters.AddWithValue("@p2", textBox35.Text);
           

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt başarıyla oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void ilsil_Click(object sender, EventArgs e)
        {

            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("delete from il where ilkodu=@p1;", baglanti); ;
            komut.Parameters.AddWithValue("@p1", int.Parse(textBox36.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show(" Silme işlemi gerçekleşti.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void ilguncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("update il set iladi=@p1 where ilkodu = @p2", baglanti);
            komut.Parameters.AddWithValue("@p1", textBox35.Text);
            komut.Parameters.AddWithValue("@p2", int.Parse(textBox36.Text));


            komut.ExecuteNonQuery();
            MessageBox.Show(" Güncelleme işlemi başarılı oldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            baglanti.Close();
        }

        private void ilara_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string sorgu = "select * from il where iladi LIKE '%" + textBox35.Text + "%'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView9.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void button9_Click(object sender, EventArgs e)  //ilcelistele
         {
            baglanti.Open();
            string sorgu = "select * from ilce";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView10.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void ilceekle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into ilce values(@p1,@p2,@p3)", baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(textBox40.Text));
            komut.Parameters.AddWithValue("@p2", textBox39.Text);
            komut.Parameters.AddWithValue("@p3", int.Parse(comboBox1.SelectedValue.ToString()));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt başarıyla oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void ilcesil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("delete from ilce where ilceid=@p1;", baglanti); ;
            komut.Parameters.AddWithValue("@p1", int.Parse(textBox40.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show(" Silme işlemi gerçekleşti.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void ilceguncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("update ilce set ilceadi=@p1 where ilceid = @p2", baglanti);
            komut.Parameters.AddWithValue("@p1", textBox39.Text);
            komut.Parameters.AddWithValue("@p2", int.Parse(textBox40.Text));


            komut.ExecuteNonQuery();
            MessageBox.Show(" Güncelleme işlemi başarılı oldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            baglanti.Close();
        }

        private void ilceara_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string sorgu = "select * from ilce where ilceadi LIKE '%" + textBox39.Text + "%'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView10.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void sigortalistele_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string sorgu = "select * from sigorta";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView8.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void sigortaekle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into sigorta values(@p1,@p2)", baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(textBox32.Text));
            komut.Parameters.AddWithValue("@p2", textBox31.Text);

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt başarıyla oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void sigortasil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("delete from sigorta where sigortaid=@p1;", baglanti); ;
            komut.Parameters.AddWithValue("@p1", int.Parse(textBox32.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show(" Silme işlemi gerçekleşti.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void sigortaguncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("update sigorta set sigortaadi=@p1 where sigortaid = @p2", baglanti);
            komut.Parameters.AddWithValue("@p1", textBox31.Text);
            komut.Parameters.AddWithValue("@p2", int.Parse(textBox32.Text));


            komut.ExecuteNonQuery();
            MessageBox.Show(" Güncelleme işlemi başarılı oldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            baglanti.Close();
        }

        private void sigortaara_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string sorgu = "select * from sigorta where sigortaadi LIKE '%" + textBox31.Text + "%'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView10.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           

        }
        private void label10_Click(object sender, EventArgs e)
        {

        }
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
