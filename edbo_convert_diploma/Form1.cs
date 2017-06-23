using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace edbo_convert_diploma
{
    public partial class Form1 : Form
    {
        string file_path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] in_words = { "розклад", "деканфак", "занятьфак", "______" };
            string[] out_words = { "num", "lastname", "name", "sex", "lastnameen", "nameen" };
            string data = @"<?xml version=""1.0"" encoding=""utf-8""?><documents>";
            // open file
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Text Files|*.xml";
            openFileDialog1.Title = "файл для обработки";
            openFileDialog1.FileName = "in.xml";
            openFileDialog1.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
            string path = file_path + "Order_2.xml";
            //file_path= file_path + "2.xml";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = openFileDialog1.FileName;
                file_path = Path.GetDirectoryName(path);
                file_path = file_path + "\\2.xml";
                //                string[] tab0 = File.ReadAllLines(path, Encoding.Default);
                //////////////////
               // textBox1.Text += path;
                String xmlString = File.ReadAllText(path);
                XDocument xdoc = XDocument.Load(new StringReader(xmlString));

                var xmlList = (from article in xdoc.Descendants("Document")
                               select new
                               {
                                   series = article.Descendants("DocumentSeries").SingleOrDefault(),
                                   number = article.Descendants("DocumentNumber").SingleOrDefault(),
                                   lastname = article.Descendants("LastName").SingleOrDefault(),
                                   firstname = article.Descendants("FirstName").SingleOrDefault(),
                                   middlename = article.Descendants("MiddleName").SingleOrDefault(),
                                   lastnameen = article.Descendants("LastNameEn").SingleOrDefault(),
                                   firstnameen = article.Descendants("FirstNameEn").SingleOrDefault(),
                                   idtypesex = article.Descendants("SexId").SingleOrDefault()
                                   //firstnameen = article.Descendants("FirstNameEn").SingleOrDefault()
                                   //firstnameen = article.Descendants("FirstNameEn").SingleOrDefault()
                                   //firstnameen = article.Descendants("FirstNameEn").SingleOrDefault()
                               }).ToList();

                var articleList = (from item in xmlList
                                   select new
                                   {
                                       series = item.series != null ? item.series.Value : null,
                                       number = item.number != null ? item.number.Value : null,
                                       lastname = item.lastname != null ? item.lastname.Value : null,
                                       firstname = item.firstname != null ? item.firstname.Value : null,
                                       middlename = item.middlename != null ? item.middlename.Value : null,
                                       lastnameen = item.lastnameen != null ? item.lastnameen.Value : null,
                                       firstnameen = item.firstnameen != null ? item.firstnameen.Value : null,
                                       idtypesex = item.idtypesex != null ? item.idtypesex.Value : null,
                                       //id = item.series != null ? item.series.Value : null,
                                       //id = item.series != null ? item.series.Value : null,
                                       //photo_s = item.photo_s != null ? item.photo_s.Value : null,
                                       //photo_m = item.photo_m != null ? item.photo_m.Value : null,
                                       //photo_l = item.photo_l != null ? item.photo_l.Value : null,
                                       //date = item.date != null ? item.date.Value : null
                                   });

                foreach (var article in articleList)
                {
                  //  textBox1.Text += article.series + article.number+article.lastname+article.firstname+article.middlename+article.lastnameen+article.firstnameen+"\r\n";
                    data += "<document>";
                    //                    data += "series=\""+article.series+"\" number=\""+article.number+"\" lastname=\"Якименко\" firstname=\"Надія\" middlename=\"Сергіївна\" fio=\"Якименко Надія Сергіївна\" lastnameen=\"Yakymenko\" firstnameen=\"Nadiia\" middlenameEn=\"Serhiivna\" fioen=\"Yakymenko Nadiia Serhiivna\" idtypesex=\"2\"";
                    data += @"
<documentdata universitynamecreate=""Ізмаїльський державний гуманітарний університет"" idorderofdocumente=""214184"" datecreate=""2017 - 02 - 08T17: 36:15.163"" iddocument=""19034036"" idordertype=""1"" type=""Диплом магістра державного зразка"" idpersoneducation=""4985493"" facultetfullname=""Педагогічний факультет"" educationformname=""Денна"" 
paymenttype=""бюджет"" series=""" + article.series + @""" 
number=""" + article.number + @""" 
lastname="""+article.lastname+@""" 
firstname="""+article.firstname+@""" 
middlename="""+article.middlename+@""" 
fio=""Коваленко Олександр Вікторович"" 
lastnameen=""" + article.lastnameen + @""" 
firstnameen=""" + article.firstnameen + @""" 
middlenameEn=""" + article.middlename + @""" 
fioen=""avk013"" 
idtypesex=""" + article.idtypesex + @"""

 birthday=""21 / 04 / 1993"" 
inn=""161316613"" pasporttype=""3"" pasportseries=""KK"" 
spasportnumber=""13131313"" 
dateendeducation=""01 / 01 / 2017"" 
universityname=""Ізмаїльський державний гуманітарний університет"" 
universitynameen=""IZMAIL STATE  UNIVERSITY OF HUMANITIES"" iduniversity=""89"" 
qualificationname=""Магістр"" qualificationnameen=""А master`s degree"" 
specdirprofcode=""8.01010201"" specdirprofname=""початкова освіта"" 
specdirprofnameen=""Primary education"" specializationname=""Передшкільна освіта"" 
educationqualificationname=""Магістр початкової освіти"" 
educationqualificationnameen=""Master of  Primary Education"" 
educationqualificationnamerod=""Магістра початкової освіти"" 
bossworkpost=""Ректор"" bossworkposten=""Rector"" boss=""Я.В. Кічук"" 
bossen=""Yaroslav Kichuk"" dategive=""28/02/2017"" award=""3"" prevdoctype=""11"" 
prevdocseries=""B15"" prevdocnumber=""134713"" />";

                    data += "</document>";
                }
                data += "</documents>";
                ////////////////
             //   data = "";
               // data = @"<?xml version=""1.0"" encoding=""utf-8""?>
 //<documents><document><documentdata universitynamecreate = ""Ізмаїльський державний гуманітарний університет"" idorderofdocumente = ""214184"" datecreate = ""2017 - 02 - 08T17: 36:15.163"" iddocument = ""19034036"" idordertype = ""1"" type = ""Диплом магістра державного зразка"" idpersoneducation = ""4985493"" facultetfullname = ""Педагогічний факультет"" educationformname = ""Денна"" paymenttype = ""бюджет"" series = ""M17"" number = ""034055"" lastname = ""Якименко"" firstname = ""Надія"" middlename = ""Сергіївна"" fio = ""Якименко Надія Сергіївна"" lastnameen = ""Yakymenko"" firstnameen = ""Nadiia"" middlenameEn = ""Serhiivna"" fioen = ""Yakymenko Nadiia Serhiivna"" idtypesex = ""2"" birthday = ""21 / 04 / 1993"" inn = ""3407912162"" pasporttype = ""3"" pasportseries = ""KM"" pasportnumber = ""604725"" dateendeducation = ""28 / 02 / 2017"" universityname = ""Ізмаїльський державний гуманітарний університет"" universitynameen = ""IZMAIL STATE  UNIVERSITY OF HUMANITIES"" iduniversity = ""89"" qualificationname = ""Магістр"" qualificationnameen = ""А master`s degree"" specdirprofcode = ""8.01010201"" specdirprofname = ""початкова освіта"" specdirprofnameen = ""Primary education"" specializationname = ""Передшкільна освіта"" educationqualificationname = ""Магістр початкової освіти, викладач &#xA;педагогіки та методики початкової освіти, &#xA;організатор передшкільної та початкової освіти"" educationqualificationnameen=""Master of  Primary Education, Teacher of Pedagogy and Methods of  Primary Education, Organizer of Preschool and  Primary Education"" educationqualificationnamerod=""Магістра початкової освіти, викладача &#xA;педагогіки та методики початкової освіти, &#xA;організатора передшкільної та початкової освіти"" bossworkpost=""Ректор"" bossworkposten=""Rector"" boss=""Я.В. Кічук"" bossen=""Yaroslav Kichuk"" dategive=""28/02/2017"" award=""3"" prevdoctype=""11"" prevdocseries=""B15"" prevdocnumber=""134713"" /></document></documents>";
                Encoding utf8WithoutBom = new UTF8Encoding(false);

                File.WriteAllText(path+"+MOD.xml", data, utf8WithoutBom);
                textBox1.Text += path+"\r\n";


            }

        }
    }
}
