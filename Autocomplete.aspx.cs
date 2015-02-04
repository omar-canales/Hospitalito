using Business.BusHospitalito;
using Business.Entity.EntHospitalito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Autocomplete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void cargarGv()
    {
        gvPers.DataSource = new BusDoctor().Obtener();
        gvPers.DataBind();
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList2(string prefixText, int count, string contextKey)
    {
        // Crea array of apellidos  
        List<EntDoctor> pers = new List<EntDoctor>();
        List<string> paternos = new List<string>();
        pers = new BusDoctor().Obtener();

        foreach (EntDoctor per in pers)
        {
            string pate = per.Paterno_Doctor;

            paternos.Add(pate);
        }

        string[] pates = paternos.ToArray();

        // Return matching apellidos  
        return (from m in pates where m.StartsWith(prefixText, StringComparison.CurrentCultureIgnoreCase) select m).Take(count).ToArray();
    }
    //protected void txtMovie_TextChanged(object sender, EventArgs e)
    //{
    //    List<EntDoctor> per = new BusDoctor().Obtener(txtMovie.Text);
    //    gvSele.DataSource = per;
    //    gvSele.DataBind();
    //}
}