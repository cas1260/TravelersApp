using AppIsphereTravelers.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace AppIsphereTravelers.Classes
{
    class clsBd
    {
        public SQLiteConnection Cn;
        public SQLiteConnection AbreBanco()
        {
            string applicationFolderPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "");
            string CaminhoDB = applicationFolderPath + "/dados.db3";
            Cn = new SQLiteConnection(CaminhoDB);
            return Cn;
        }

        public void ApagarTable()
        {
            Cn.DropTable<UserLogin>();
        }
    }
}
