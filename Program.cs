using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infagazatiemelt2017
{
    class IdozitettFelirat
    {
        string idozites;
        string felirat;
        public string Felirat
        {
            get { return felirat;  }
        }
        public IdozitettFelirat(string idozites, string felirat)
        {
            this.idozites = idozites;
            this.felirat = felirat;
        }
        //rövidebb megoldás
        public int SzavakSzama
        {
            get { return felirat.Split(' ').Length + 1; }
        } 
        //általános megoldás
        public int SzavakSzama2
        {
            get
            {
                int db = 1;
                foreach (char item in felirat)
                {
                    if (item == ' ') db++;
                }
                return db;
            }
        }

        public string SrtIdozites()
        {
            string srtido = "";
            srtido += ConverterMinSec_HMinSec(idozites.Split(' ')[0]) + " --> ";
            srtido += ConverterMinSec_HMinSec(idozites.Split(' ')[2]);
            return srtido;
        }
        private string ConverterMinSec_HMinSec(string ido)
        { //05:12         00:05:12
            string srt = "0";
            int min = int.Parse(ido.Split(':')[0]);
            string sec = ido.Split(':')[1];
            srt += min / 60 + ":";
            if (min % 60 > 9)
                srt += min % 60;
            else
                srt += "0" + min % 60;
            srt += ":" + sec;
            return srt;
        }

    }
    class Program
    {
        static List<IdozitettFelirat> FeliratokLista = new List<IdozitettFelirat>();
        static void SrtKiir(string fn)
        {
            StreamWriter sw = new StreamWriter(fn);
            for (int i = 0; i < FeliratokLista.Count; i++)
            {
                sw.WriteLine(i + 1);
                sw.WriteLine(FeliratokLista[i].SrtIdozites());
                sw.WriteLine(FeliratokLista[i].Felirat);
                sw.WriteLine();
            }
            sw.Flush();
            sw.Close();
        }
        static void Beolvasas(string fn)
        {
            StreamReader sr = new StreamReader(fn);
            while (!sr.EndOfStream)
            {
                FeliratokLista.Add(new IdozitettFelirat(sr.ReadLine(), sr.ReadLine()));
            }
            sr.Close();
        }
        static string LegtobbSzavas()
        {
            int legtobb_index = 0;
            for (int i = 0; i < FeliratokLista.Count; i++)
            {
                if(FeliratokLista[i].SzavakSzama > FeliratokLista[legtobb_index].SzavakSzama)
                {
                    legtobb_index = i;
                }
            }
            return FeliratokLista[legtobb_index].Felirat;
        }
        static void Main(string[] args)
        {
            Beolvasas("feliratok.txt");
            Console.WriteLine("5.feladat: Feliratok száma:" + FeliratokLista.Count);
            Console.WriteLine("7.feladat: Legtöbb szavas felirat:\n" + LegtobbSzavas());
            SrtKiir("felirat.srt");
            Console.ReadKey();
        }
    }
}
