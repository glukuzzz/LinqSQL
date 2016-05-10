using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;

namespace LinqSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            

            SqlConnectionStringBuilder sconbuilder = new SqlConnectionStringBuilder();

            sconbuilder.IntegratedSecurity = true;
            sconbuilder.DataSource = "HOST";
            sconbuilder.InitialCatalog = "CSharp";
            SqlConnection sconn = new SqlConnection(sconbuilder.ConnectionString);

            sconn.Open();
            SqlCommand cmd = sconn.CreateCommand();
            //cmd.CommandText = "create database CSharp on (name = 'CSharp', filename = 'c:\\DB\\csharp.mdf', size = 10MB, maxsize = 100MB, filegrowth = 1MB)";
            cmd.CommandText = "create table Users (UserID int identity (1,1) primary key clustered, Name nvarchar(30) not null, Surname nvarchar(30) not null, Description nvarchar(1000) null, RegistrationDate datetime not null default (getdate())  )";
            cmd.ExecuteNonQuery();
            Console.ReadKey();
        }

        private static void GetNamesFromFB()
        {
            DataClasses2DataContext data = new DataClasses2DataContext();
            var a = DateTime.Now;
            List<Cities> city = new List<Cities>();
            city.AddRange(data.Cities);


            Console.WriteLine(DateTime.Now - a);
        }

        private static void WordGame()
        {   string a = "asdasd";
            char lastChar = 'А';
            DataClasses2DataContext data = new DataClasses2DataContext();
            
            
            

            while (true)
            {
                a = data.Cities.Where(x => x.Name.StartsWith(Convert.ToString(lastChar))).FirstOrDefault().Name;
                
                Console.WriteLine(a);
                if (a.LastOrDefault() == 'ь' || a.LastOrDefault() == 'ъ')
                {
                    a = a.Remove(a.Count() - 1);
                }
                a = a.ToUpper();
                lastChar = a.LastOrDefault();

                while (true)
                {
                    a = Console.ReadLine();
                    if (data.Cities.Contains(new Cities { Name = a }))
                    {
                        if (a.LastOrDefault() == 'ь' || a.LastOrDefault() == 'ъ')
                        {
                            a = a.Remove(a.Count() - 1);
                        }
                        a = a.ToUpper();
                        lastChar = a.LastOrDefault();
                        
                        goto link1;
                    }
                    link1:;
                }
                
            }
        }




        private static void InitDataBase()
        {
            DataClasses2DataContext data = new DataClasses2DataContext();

            foreach (string a in CityNames.names)
            {

                Cities newCity = new Cities { Name = a };
                data.Cities.InsertOnSubmit(newCity);

            }
            data.SubmitChanges();


            foreach (var a in data.Cities)
            {
                Console.WriteLine(a.Name);
            }

            Console.WriteLine(data.Cities.Count());
        }
    }
}
