using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;

namespace pizarra
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();

            
        }

        int imagenes;
        int imagenes2;

        SqlConnection con = new SqlConnection(@"Data Source = 192.168.100.13; Initial Catalog = CLINIWIN; Persist Security Info=True;User ID = TURGENCIA; Password=184114518");


        


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        

        private void Form1_Load(object sender, EventArgs e)
        {

            timer1_Tick(null, null);

        }



      

        private void enTotal_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {

           
           // pictureBox2.Width = this.Width - imagenes2;

           // pictureBox1.Width = this.Width - 800;
           // pictureBox2.Width = this.Width - 1600;
           //pictureBox1.Height = this.Height - 1;




        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }


        private void timer1_Tick(object sender, EventArgs e)
        {

            cargar_Click(null,null);
        }

      

        private void cargar_Click(object sender, EventArgs e)
        {

            con.Open();

            SqlCommand sc = new SqlCommand("SELECT COUNT(INGRESO.PAC_NUMFICHA) FROM INGRESO (nolock) LEFT JOIN FICHA  (nolock) ON FICHA.PAC_NUMFICHA = INGRESO.PAC_NUMFICHA LEFT JOIN PACIENTE  (nolock) ON FICHA.PAC_CORREL = PACIENTE.PAC_CORREL LEFT JOIN PRESTADOR  (nolock) ON INGRESO.PTD_RUT_TRAT = PRESTADOR.PTD_RUT WHERE FICHA.PAC_TIPO = 'U' AND datediff(YEAR,PAC_FECNAC,GETDATE()) > 15 AND ING_CATEGORIZACION IS NULL AND INGRESO.SER_CODIGO = 'URG'AND INGRESO.ING_HORAING > DATEADD(D, -1, GETDATE())AND INGRESO.ING_HORAALTA IS NULL AND INGRESO.ING_FECCIER IS NULL AND NOT  EXISTS ( SELECT * FROM CAMILLA_ASIGNADA  (nolock) WHERE INGRESO.PAC_NUMFICHA = CAMILLA_ASIGNADA.PAC_NUMFICHA  AND INGRESO.ING_CORREL = CAMILLA_ASIGNADA.ING_CORREL  AND CAMILLA_ASIGNADA.CML_CODIGO IS NOT NULL )", con);
            /// n° pacientes en espera adultos

            sc.ExecuteNonQuery();
            int scc = ((int)sc.ExecuteScalar());
            sinc.Text = scc.ToString();

            SqlCommand scn = new SqlCommand("SELECT COUNT(INGRESO.PAC_NUMFICHA) FROM INGRESO (nolock) LEFT JOIN FICHA  (nolock) ON FICHA.PAC_NUMFICHA = INGRESO.PAC_NUMFICHA LEFT JOIN PACIENTE  (nolock) ON FICHA.PAC_CORREL = PACIENTE.PAC_CORREL LEFT JOIN PRESTADOR  (nolock) ON INGRESO.PTD_RUT_TRAT = PRESTADOR.PTD_RUT WHERE FICHA.PAC_TIPO = 'U' AND datediff(YEAR,PAC_FECNAC,GETDATE()) < 15 AND ING_CATEGORIZACION IS NULL AND INGRESO.SER_CODIGO = 'URG'AND INGRESO.ING_HORAING > DATEADD(D, -1, GETDATE())AND INGRESO.ING_HORAALTA IS NULL AND INGRESO.ING_FECCIER IS NULL AND NOT  EXISTS ( SELECT * FROM CAMILLA_ASIGNADA  (nolock) WHERE INGRESO.PAC_NUMFICHA = CAMILLA_ASIGNADA.PAC_NUMFICHA  AND INGRESO.ING_CORREL = CAMILLA_ASIGNADA.ING_CORREL  AND CAMILLA_ASIGNADA.CML_CODIGO IS NOT NULL )", con);
            /// n° pacientes en espera niños

            scn.ExecuteNonQuery();
            int sccn = ((int)scn.ExecuteScalar());
            sincn.Text = sccn.ToString();



            SqlCommand at = new SqlCommand("SELECT COUNT(INGRESO.PAC_NUMFICHA)  FROM INGRESO (nolock) LEFT JOIN ATENCION (NOLOCK) ON  ATENCION.PAC_NUMFICHA = INGRESO.PAC_NUMFICHA AND ATENCION.ING_CORREL = INGRESO.ING_CORREL LEFT JOIN FICHA  (nolock) ON FICHA.PAC_NUMFICHA = INGRESO.PAC_NUMFICHA LEFT JOIN PACIENTE  (nolock) ON FICHA.PAC_CORREL = PACIENTE.PAC_CORREL LEFT JOIN PRESTADOR  (nolock) ON INGRESO.PTD_RUT_TRAT = PRESTADOR.PTD_RUT LEFT JOIN (SELECT * FROM REG_PROCED_ENFERMERIA where RPE_CORREL=2) AS HORA_ENFERMERA ON HORA_ENFERMERA.PAC_NUMFICHA=INGRESO.PAC_NUMFICHA AND HORA_ENFERMERA.ING_CORREL=INGRESO.ING_CORREL LEFT JOIN USUARIO ON USUARIO.USU_LOGIN=HORA_ENFERMERA.USU_LOGIN LEFT JOIN ( SELECT CMA_CORREL,PAC_NUMFICHA,ING_CORREL,CML_CODIGO FROM CAMILLA_ASIGNADA CA WHERE CMA_CORREL = ( SELECT TOP 1 CMA_CORREL FROM CAMILLA_ASIGNADA CAM WHERE CAM.PAC_NUMFICHA=CA.PAC_NUMFICHA AND CAM.ING_CORREL=CA.ING_CORREL ORDER BY CAM.CMA_CORREL DESC))AS ULTIMOBOX ON ULTIMOBOX.PAC_NUMFICHA=INGRESO.PAC_NUMFICHA AND ULTIMOBOX.ING_CORREL=INGRESO.ING_CORREL 	LEFT JOIN ( SELECT CMA_CORREL,PAC_NUMFICHA,ING_CORREL,CMA_HORADESDE FROM CAMILLA_ASIGNADA CA WHERE CMA_CORREL = ( SELECT TOP 1 CMA_CORREL FROM CAMILLA_ASIGNADA CAM WHERE CAM.PAC_NUMFICHA=CA.PAC_NUMFICHA AND CAM.ING_CORREL=CA.ING_CORREL  ORDER BY CAM.CMA_CORREL ASC))AS PRIMERAHORA 	ON PRIMERAHORA.PAC_NUMFICHA=INGRESO.PAC_NUMFICHA AND PRIMERAHORA.ING_CORREL=INGRESO.ING_CORREL 	WHERE FICHA.PAC_TIPO = 'U' AND INGRESO.SER_CODIGO = 'URG' AND INGRESO.ING_HORAING > DATEADD(D, -1, GETDATE())AND INGRESO.ING_HORAALTA IS NULL AND INGRESO.ING_FECCIER IS NULL AND EXISTS (SELECT * FROM CAMILLA_ASIGNADA (nolock) WHERE INGRESO.PAC_NUMFICHA = CAMILLA_ASIGNADA.PAC_NUMFICHA AND INGRESO.ING_CORREL = CAMILLA_ASIGNADA.ING_CORREL AND CAMILLA_ASIGNADA.CML_CODIGO IS NOT NULL AND CAMILLA_ASIGNADA.CMA_HORAHASTA IS NULL) ", con);
            /// n° pacientes en BOX

            at.ExecuteNonQuery();
            int att = ((int)at.ExecuteScalar());
            enAtencion.Text = att.ToString();



            SqlCommand c4 = new SqlCommand("SELECT count(ING_CATEGORIZACION) as cantidad FROM INGRESO (nolock) LEFT JOIN FICHA  (nolock) ON FICHA.PAC_NUMFICHA = INGRESO.PAC_NUMFICHA LEFT JOIN PACIENTE (NOLOCK) ON PACIENTE.PAC_CORREL=FICHA.PAC_CORREL  WHERE FICHA.PAC_TIPO = 'U'  AND INGRESO.SER_CODIGO = 'URG' AND ING_CATEGORIZACION IN ('C4') AND datediff(YEAR,PAC_FECNAC,GETDATE()) > 15 AND INGRESO.ING_HORAING > DATEADD(D, -1, GETDATE())AND INGRESO.ING_HORAALTA IS NULL AND INGRESO.ING_FECCIER IS NULL AND NOT  EXISTS ( SELECT * 	 FROM CAMILLA_ASIGNADA  (nolock) 	 WHERE INGRESO.PAC_NUMFICHA = CAMILLA_ASIGNADA.PAC_NUMFICHA 	 AND INGRESO.ING_CORREL = CAMILLA_ASIGNADA.ING_CORREL 	 AND CAMILLA_ASIGNADA.CML_CODIGO IS NOT NULL ) ", con);
            ///ADULTO c4

            c4.ExecuteNonQuery();
            int c44 = ((int)c4.ExecuteScalar());
            mac4.Text = c44.ToString();


            SqlCommand c4n = new SqlCommand("SELECT count(ING_CATEGORIZACION) as cantidad FROM INGRESO (nolock) LEFT JOIN FICHA  (nolock) ON FICHA.PAC_NUMFICHA = INGRESO.PAC_NUMFICHA LEFT JOIN PACIENTE (NOLOCK) ON PACIENTE.PAC_CORREL=FICHA.PAC_CORREL  WHERE FICHA.PAC_TIPO = 'U'  AND INGRESO.SER_CODIGO = 'URG' AND ING_CATEGORIZACION IN ('C4') AND datediff(YEAR,PAC_FECNAC,GETDATE()) < 15 AND INGRESO.ING_HORAING > DATEADD(D, -1, GETDATE())AND INGRESO.ING_HORAALTA IS NULL AND INGRESO.ING_FECCIER IS NULL AND NOT  EXISTS ( SELECT * 	 FROM CAMILLA_ASIGNADA  (nolock) 	 WHERE INGRESO.PAC_NUMFICHA = CAMILLA_ASIGNADA.PAC_NUMFICHA 	 AND INGRESO.ING_CORREL = CAMILLA_ASIGNADA.ING_CORREL 	 AND CAMILLA_ASIGNADA.CML_CODIGO IS NOT NULL ) ", con);
            ///NIÑOS c4

            c4n.ExecuteNonQuery();
            int c44n = ((int)c4n.ExecuteScalar());
            mnc4.Text = c44n.ToString();



            SqlCommand c5 = new SqlCommand("SELECT count(ING_CATEGORIZACION) as cantidad FROM INGRESO (nolock) LEFT JOIN FICHA  (nolock) ON FICHA.PAC_NUMFICHA = INGRESO.PAC_NUMFICHA LEFT JOIN PACIENTE (NOLOCK) ON PACIENTE.PAC_CORREL=FICHA.PAC_CORREL  WHERE FICHA.PAC_TIPO = 'U'  AND INGRESO.SER_CODIGO = 'URG' AND ING_CATEGORIZACION IN ('C5') AND datediff(YEAR,PAC_FECNAC,GETDATE()) > 15 AND INGRESO.ING_HORAING > DATEADD(D, -1, GETDATE())AND INGRESO.ING_HORAALTA IS NULL AND INGRESO.ING_FECCIER IS NULL AND NOT  EXISTS ( SELECT * 	 FROM CAMILLA_ASIGNADA  (nolock) 	 WHERE INGRESO.PAC_NUMFICHA = CAMILLA_ASIGNADA.PAC_NUMFICHA 	 AND INGRESO.ING_CORREL = CAMILLA_ASIGNADA.ING_CORREL 	 AND CAMILLA_ASIGNADA.CML_CODIGO IS NOT NULL ) ", con);
            ///ADULTO c5

            c5.ExecuteNonQuery();
            int c55 = ((int)c5.ExecuteScalar());
            mac55.Text = c55.ToString();



            SqlCommand c5n = new SqlCommand("SELECT count(ING_CATEGORIZACION) as cantidad FROM INGRESO (nolock) LEFT JOIN FICHA  (nolock) ON FICHA.PAC_NUMFICHA = INGRESO.PAC_NUMFICHA LEFT JOIN PACIENTE (NOLOCK) ON PACIENTE.PAC_CORREL=FICHA.PAC_CORREL  WHERE FICHA.PAC_TIPO = 'U'  AND INGRESO.SER_CODIGO = 'URG' AND ING_CATEGORIZACION IN ('C5') AND datediff(YEAR,PAC_FECNAC,GETDATE()) < 15 AND INGRESO.ING_HORAING > DATEADD(D, -1, GETDATE())AND INGRESO.ING_HORAALTA IS NULL AND INGRESO.ING_FECCIER IS NULL AND NOT  EXISTS ( SELECT * 	 FROM CAMILLA_ASIGNADA  (nolock) 	 WHERE INGRESO.PAC_NUMFICHA = CAMILLA_ASIGNADA.PAC_NUMFICHA 	 AND INGRESO.ING_CORREL = CAMILLA_ASIGNADA.ING_CORREL 	 AND CAMILLA_ASIGNADA.CML_CODIGO IS NOT NULL ) ", con);
            ///NIÑO c5

            c5n.ExecuteNonQuery();
            int c55n = ((int)c5n.ExecuteScalar());
            MNC5N.Text = c55n.ToString();




            SqlCommand c3 = new SqlCommand(" SELECT count(ING_CATEGORIZACION) as cantidad FROM INGRESO (nolock) LEFT JOIN FICHA  (nolock) ON FICHA.PAC_NUMFICHA = INGRESO.PAC_NUMFICHA LEFT JOIN PACIENTE (NOLOCK) ON PACIENTE.PAC_CORREL=FICHA.PAC_CORREL  WHERE FICHA.PAC_TIPO = 'U'  AND INGRESO.SER_CODIGO = 'URG' AND ING_CATEGORIZACION IN ('C3') AND datediff(YEAR,PAC_FECNAC,GETDATE()) > 15 AND INGRESO.ING_HORAING > DATEADD(D, -1, GETDATE())AND INGRESO.ING_HORAALTA IS NULL AND INGRESO.ING_FECCIER IS NULL AND NOT  EXISTS ( SELECT * 	 FROM CAMILLA_ASIGNADA  (nolock) 	 WHERE INGRESO.PAC_NUMFICHA = CAMILLA_ASIGNADA.PAC_NUMFICHA 	 AND INGRESO.ING_CORREL = CAMILLA_ASIGNADA.ING_CORREL 	 AND CAMILLA_ASIGNADA.CML_CODIGO IS NOT NULL ) ", con);
            ///adulto c3

            c3.ExecuteNonQuery();
            int c33 = ((int)c3.ExecuteScalar());
            mac3.Text = c33.ToString();

            SqlCommand c3n = new SqlCommand(" SELECT count(ING_CATEGORIZACION) as cantidad FROM INGRESO (nolock) LEFT JOIN FICHA  (nolock) ON FICHA.PAC_NUMFICHA = INGRESO.PAC_NUMFICHA LEFT JOIN PACIENTE (NOLOCK) ON PACIENTE.PAC_CORREL=FICHA.PAC_CORREL  WHERE FICHA.PAC_TIPO = 'U'  AND INGRESO.SER_CODIGO = 'URG' AND ING_CATEGORIZACION IN ('C3') AND datediff(YEAR,PAC_FECNAC,GETDATE()) < 15 AND INGRESO.ING_HORAING > DATEADD(D, -1, GETDATE())AND INGRESO.ING_HORAALTA IS NULL AND INGRESO.ING_FECCIER IS NULL AND NOT  EXISTS ( SELECT * 	 FROM CAMILLA_ASIGNADA  (nolock) 	 WHERE INGRESO.PAC_NUMFICHA = CAMILLA_ASIGNADA.PAC_NUMFICHA 	 AND INGRESO.ING_CORREL = CAMILLA_ASIGNADA.ING_CORREL 	 AND CAMILLA_ASIGNADA.CML_CODIGO IS NOT NULL ) ", con);
            ///NIÑO c3

            c3n.ExecuteNonQuery();
            int c33n = ((int)c3n.ExecuteScalar());
            mnc3.Text = c33n.ToString();





            SqlCommand c2 = new SqlCommand("SELECT count(ING_CATEGORIZACION) as cantidad FROM INGRESO (nolock) LEFT JOIN FICHA  (nolock) ON FICHA.PAC_NUMFICHA = INGRESO.PAC_NUMFICHA LEFT JOIN PACIENTE (NOLOCK) ON PACIENTE.PAC_CORREL=FICHA.PAC_CORREL  WHERE FICHA.PAC_TIPO = 'U'  AND INGRESO.SER_CODIGO = 'URG' AND ING_CATEGORIZACION IN ('C2') AND datediff(YEAR,PAC_FECNAC,GETDATE()) > 15 AND INGRESO.ING_HORAING > DATEADD(D, -1, GETDATE())AND INGRESO.ING_HORAALTA IS NULL AND INGRESO.ING_FECCIER IS NULL AND NOT  EXISTS ( SELECT * 	 FROM CAMILLA_ASIGNADA  (nolock) 	 WHERE INGRESO.PAC_NUMFICHA = CAMILLA_ASIGNADA.PAC_NUMFICHA 	 AND INGRESO.ING_CORREL = CAMILLA_ASIGNADA.ING_CORREL 	 AND CAMILLA_ASIGNADA.CML_CODIGO IS NOT NULL ) ", con);
            /// ADULTO C2

            c2.ExecuteNonQuery();
            int c22 = ((int)c2.ExecuteScalar());
            mac2.Text = c22.ToString();

            SqlCommand c2n = new SqlCommand("SELECT count(ING_CATEGORIZACION) as cantidad FROM INGRESO (nolock) LEFT JOIN FICHA  (nolock) ON FICHA.PAC_NUMFICHA = INGRESO.PAC_NUMFICHA LEFT JOIN PACIENTE (NOLOCK) ON PACIENTE.PAC_CORREL=FICHA.PAC_CORREL  WHERE FICHA.PAC_TIPO = 'U'  AND INGRESO.SER_CODIGO = 'URG' AND ING_CATEGORIZACION IN ('C2') AND datediff(YEAR,PAC_FECNAC,GETDATE()) < 15 AND INGRESO.ING_HORAING > DATEADD(D, -1, GETDATE())AND INGRESO.ING_HORAALTA IS NULL AND INGRESO.ING_FECCIER IS NULL AND NOT  EXISTS ( SELECT * 	 FROM CAMILLA_ASIGNADA  (nolock) 	 WHERE INGRESO.PAC_NUMFICHA = CAMILLA_ASIGNADA.PAC_NUMFICHA 	 AND INGRESO.ING_CORREL = CAMILLA_ASIGNADA.ING_CORREL 	 AND CAMILLA_ASIGNADA.CML_CODIGO IS NOT NULL ) ", con);
            /// NIÑO C2

            c2n.ExecuteNonQuery();
            int c22n = ((int)c2n.ExecuteScalar());
            mnc2.Text = c22n.ToString();


            enEspera.Text = (c22 + c22n + c33 + c33n + c44 + c44n + c55 + c55n + sccn + scc).ToString();
            /// n° pacientes en espera 


            enTotal.Text = (att + sccn + scc + c44 + c44n + c33 + c33n + c22 + c22n + c55 + c55n).ToString();
            //total pacientes

            totala.Text = (c22 + c33 + c44 + c55 + scc).ToString();
            totaln.Text = (c22n + c33n + c44n + c55n + sccn).ToString();



            con.Close();

            DateTime t = DateTime.Now;

            fecha.Text = t.ToString("D", CultureInfo.CreateSpecificCulture("es-CL"));
            hora.Text = t.ToString("t", CultureInfo.CreateSpecificCulture("es-CL"));





        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }
    }
}
