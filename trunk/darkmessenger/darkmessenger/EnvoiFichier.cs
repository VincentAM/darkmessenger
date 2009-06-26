using System;
using System.Collections;
using System.IO;
using System.Text;

namespace darkmessenger
{
    class EnvoiFichier
    {

        public string s_nom_fichier;
        public string s_nom_fichier_sans_path;
        public double i_nb_block_fichier;
        public double i_taille_block;
        public double i_taille_eof_block;
        private FileStream fs_fichier;
        public string s_from;
        public string s_to;

        public EnvoiFichier(string s_url_fichier, string _s_from, string _s_to)
        {
            s_nom_fichier = s_url_fichier;
            s_nom_fichier_sans_path = s_url_fichier.Split('\\')[s_url_fichier.Split('\\').Length-1];
            s_from = _s_from;
            s_to = _s_to;
            fs_fichier = new FileStream(s_nom_fichier,FileMode.Open);

            i_taille_block=2048;

            i_nb_block_fichier=Math.Ceiling(fs_fichier.Length/i_taille_block);
            i_taille_eof_block = fs_fichier.Length % i_taille_block;
            Console.WriteLine("nb:"+i_nb_block_fichier+" eof:"+i_taille_eof_block);
            fs_fichier.Close();
            fs_fichier.Dispose();
        }

        public string getTrame()
        {
            return TrameClient.getSendFileTrame(s_from, s_to, i_taille_block.ToString(), i_taille_eof_block.ToString(), i_nb_block_fichier.ToString(),s_nom_fichier_sans_path);
        }


    }
}
