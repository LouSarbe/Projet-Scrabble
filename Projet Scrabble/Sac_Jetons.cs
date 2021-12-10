using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Scrabble
{
    class Sac_Jetons
    {
        /*
        A;1;9
        B;3;2
        C;3;2
        D;2;3
        E;1;15
        F;4;2
        G;2;2
        H;4;2
        I;1;8
        J;8;1
        K;10;1
        L;1;5
        M;2;3
        N;1;6
        O;1;6
        P;3;2
        Q;8;1
        R;1;6
        S;1;6
        T;1;6
        U;1;6
        V;4;2
        W;10;1
        X;10;1
        Y;10;1
        Z;10;1
        *;0;2
        */
        //Déclaration
        List<Jeton> sac;
        int nombre;

        //Propriétés
        public List<char> Sac
        {
            get { return sac; }
        }

        //Constructeur
        public Sac_Jetons()
        {
            
        }

        //Opérations
        public Jeton Retire_Jeton(Random r)
        {
            Jeton ret;
            int n = r.Next(sac.Count);
            ret = sac[n];
            sac.RemoveAt(n);
            return ret;
        }
        public string toString()
        {
            string ret = "";
            for(int i = 0; i < nombre; i++)
            {
                
            }
            return ret;
        }
    }
}
