using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSakk
{
    public class Jatszma
    {
        static Dictionary<string, byte> dict = new()
        {
            {"a", 1},
            {"b", 2},
            {"c", 3},
            {"d", 4},
            {"e", 5},
            {"f", 6},
            {"g", 7},
            {"h", 8}
        };
        

        List<String> lepesek;
        //todo Állapottér reprezentáció kialakítása V2.0

        /// <summary>
        /// Üres játék létrehozása
        /// </summary>
        public Jatszma()
        {
            lepesek = new List<String>();
        }
        public Jatszma(String fajlSor)
        {
            lepesek = new List<String>();
            foreach (var item in fajlSor.Trim().Split('\t'))
            {
                lepesek.Add(item);
            }
        }

        public int LepesekSzama => lepesek.Count();

        public char Nyertes => LepesekSzama % 2 == 0 ? 's' : 'v';

        //public int HuszarokLepesszama => lepesek.Count(lepes => lepes[0] == 'H');
        public int HuszarokLepesszama => TisztLepesszama('H');

        public int TisztLepesszama(char tisztJele)
        {
            return lepesek.Count(lepes => lepes[0] == tisztJele);
        }

        /// <summary>
        /// todo: Keresse meg mindkét vezér (királynő) utolsó pozícióját és nézze meg, hogy ott ütötték-e ezt a pozíviót? (vmi x poz)
        /// </summary>

        public int BabukSzama => 32 - lepesek.Count(n => n.Contains('x'));

        public bool FeherKiralyMozgas => FeherKiralyMozgott();

        public bool FeherKiralyMozgott()
        {
            for (int i = 0; i < lepesek.Count(); i++) if (i%2==0 && (lepesek[i].Contains('K') || lepesek[i].Contains('O'))) return true;
            return false;
        }


        public bool VezerLeutve()
        {
            string vilagosVezerPoz = "d1";
            string sotetVezerPoz = "d8";
            for (int i = 0; i < lepesek.Count(); i++)
            {
                if (lepesek[i].Contains('x') && (lepesek[i][^2..] == sotetVezerPoz || lepesek[i][^2..] == vilagosVezerPoz)) return true;
                if (i % 2 == 0 && lepesek[i].Contains('V')) vilagosVezerPoz = lepesek[i][^2..];
                else if (i % 2 == 1 && lepesek[i].Contains('V')) sotetVezerPoz = lepesek[i][^2..];
            }

            return false;
        }
        public bool VezerUtes => VezerLeutve();

        public int Vezerlepesek => VezerLepesekSzama();


        public int VezerLepesekSzama()
        {
            string vilagosVezerPoz = "d1";
            string sotetVezerPoz = "d8";
            int lepesekSzama = 0;
            for (int i = 0; i < lepesek.Count(); i++)
            {
                if (i % 2 == 0 && lepesek[i].Contains('V'))
                {
                    if (vilagosVezerPoz[1] == lepesek[i][^1])
                    {
                        lepesekSzama += Math.Abs(dict[vilagosVezerPoz[0].ToString()] - dict[lepesek[i][^2].ToString()]);
                    }


                    vilagosVezerPoz = lepesek[i][^2..];
                }
                else if (i % 2 == 1 && lepesek[i].Contains('V'))
                {
                    sotetVezerPoz = lepesek[i][^2..];
                }
            }


            return 10;
        }





    }
}
