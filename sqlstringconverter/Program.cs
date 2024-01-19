public class User 
{
    public int Id {  get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}



//UI-ból megszerezhetők az adatok, amik a függvényekhez kellenek, ha úgy van felépítve az adatbázis is,
//pl tábla neve a modell neve is, ezenkívül a textbox-ok-ban megadhatók h miket mire akarjunk lecserélni.
public class ConvertToSQL { 

    //items = milyen mezőket akarunk lekérdezni
    //parameter1 = melyik mező alapján keressünk
    //parameter2 = keresett mező értéke
    public string GetToSQL(User user,List<string> items , List<string> parameter1, List<string> parameter2) {
        string result = "SELECT ";
        for (int i = 0; i < items.Count; i++) { 
            result += items[i].ToString().ToUpper();
            result += " ";
            if (items.Count >= 2 && i != items.Count - 1) {
                result += "AND ";
            }
        }
        result += "FROM ";
        result += typeof(User).Name.ToString().ToUpper();
        result += " WHERE ";
        for (int i = 0; i < parameter1.Count; i++)
        {
            result += parameter1[i].ToUpper();
            result += " =";
            result += "'";
            result += parameter2[i];
            result += "'";
            if (parameter1.Count >= 2 && i != parameter1.Count - 1)
            {
                result += " AND ";
            }
        }
        result += ";";
        return result;
    }
   
    //change1 = megváltoztandó mező neve
    //cahnge2 = mire változtassuk meg
    //parameter1 = melyik mező alapján keressünk
    //parameter2 = keresett mező értéke
    public string UpdateSQLMaker(User user,List<string> change1, List<string> change2, List<string> parameter1, List<string> parameter2)
    {
        string result = "UPDATE ";
        result += typeof(User).Name.ToString().ToUpper();
        result += " SET ";
        for(int i = 0; i < change1.Count;i++)
        {
            result += change1[i].ToUpper();
            result += " = ";
            result += "'";
            result += change2[i];
            result += "'";
            result += " ";
            if (change1.Count >= 2 && i != change1.Count-1) {
                result += ",";
            }
        }
        result += "WHERE ";
        for (int i = 0; i < parameter1.Count; i++) 
        {
            result += parameter1[i].ToUpper();
            result += " =";
            result += "'";
            result += parameter2[i];
            result += "'";
            if (parameter1.Count >= 2 && i != parameter1.Count-1) {
                result += " AND ";
            }
        }
        result += ";";
        return result;
    }
}


public class Program {
    public static void Main(string[] args)
    {
        var User = new User()
        {
            Id = 1,
            Name = "Test",
            Email = "Testeamil",
            Password = "Password"
        };

        var ConvertToSQL = new ConvertToSQL();
        var changelist1 = new List<string>();
        changelist1.Add("Name");
        changelist1.Add("Email");

        var changelist2 = new List<string>();
        changelist2.Add("ujnevem");
        changelist2.Add("ujjelszavam");

        var parameterlist1 = new List<string>();
        parameterlist1.Add("NAME");
        parameterlist1.Add("EMAIL");

        var parameterlist2 = new List<string>();
        parameterlist2.Add("test");
        parameterlist2.Add("myemail");

        Console.WriteLine(ConvertToSQL.UpdateSQLMaker(User, changelist1,changelist2, parameterlist1, parameterlist2));

        var items = new List<string>();
        items.Add("NAME");
        items.Add("PASSWORD");

        Console.WriteLine(ConvertToSQL.GetToSQL(User,items,parameterlist1,parameterlist2));
    }
}