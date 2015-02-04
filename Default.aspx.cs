using Business.BusHospitalito;
using Business.Entity.EntHospitalito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenargvDoctores();
                LlenarchkTurno();
                LlenarrbIntervencion();
                LlenarddlSexo();
            }
        }
        public void LlenarddlSexo()
        {
            DropDownList ddlSexo = (DropDownList)gvDoctores.FooterRow.FindControl("ddlSexoFT");
            ddlSexo.DataSource = new BusSexo().Obtener();
            ddlSexo.DataTextField = "Nombre_Sexo";
            ddlSexo.DataValueField = "Id_Sexo";
            ddlSexo.DataBind();
        }
        public void LlenarrbIntervencion()
        {
            RadioButtonList rbinter = (RadioButtonList)gvDoctores.FooterRow.FindControl("rbInterFT");
            rbinter.DataSource = new BusIntervencion().Obtener();
            rbinter.DataTextField = "Nombre_Intervencion";
            rbinter.DataValueField = "Id_Intervencion";
            rbinter.DataBind();
        }
        public void LlenarchkTurno()
        {
            CheckBoxList chkTurno = (CheckBoxList)gvDoctores.FooterRow.FindControl("chkTurnoFT");
            chkTurno.DataSource = new BusTurno().Obtener();
            chkTurno.DataTextField = "Nombre_Turno";
            chkTurno.DataValueField = "Id_Turno";
            chkTurno.DataBind();
        }
        #region gvDoctores
        public void LlenargvDoctores()
        {
            gvDoctores.DataSource = new BusDoctor().Obtener();
            gvDoctores.DataBind();
        }
        protected void gvDoctores_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gvDoctores.EditIndex = e.NewEditIndex;// cambiar a modo editar
                LlenargvDoctores();//recargar el gv
                DropDownList ddlSexo = (DropDownList)gvDoctores.Rows[e.NewEditIndex].FindControl("ddlSexoEIT");
                ddlSexo.DataSource = new BusSexo().Obtener();
                ddlSexo.DataTextField = "Nombre_Sexo";
                ddlSexo.DataValueField = "Id_Sexo";
                ddlSexo.DataBind();
                ddlSexo.SelectedValue = gvDoctores.DataKeys[e.NewEditIndex].Values["Id_Sexo_Doctor"].ToString();

                RadioButtonList rbinter = (RadioButtonList)gvDoctores.Rows[e.NewEditIndex].FindControl("rbInterEIT");
                rbinter.DataSource = new BusIntervencion().Obtener();
                rbinter.DataTextField = "Nombre_Intervencion";
                rbinter.DataValueField = "Id_Intervencion";
                rbinter.DataBind();
                rbinter.DataSource = gvDoctores.DataKeys[e.NewEditIndex].Values["Id_Intervencion_Doctor"].ToString();

                CheckBoxList chkTurno = (CheckBoxList)gvDoctores.Rows[e.NewEditIndex].FindControl("chkTurnoEIT");
                chkTurno.DataSource = new BusTurno().Obtener();
                chkTurno.DataTextField = "Nombre_Turno";
                chkTurno.DataValueField = "Id_Turno";
                chkTurno.DataBind();
                chkTurno.DataSource = gvDoctores.DataKeys[e.NewEditIndex].Values["Id_Turno_Doctor"].ToString();
            }
            catch (Exception ex)
            {

                MostrarMensaje(ex.Message);
            }
        }


        protected void gvDoctores_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gvDoctores.DataKeys[e.RowIndex].Values["Id_Doctor"]);
                new BusDoctor().Eliminar(id);
                Response.Redirect(Request.CurrentExecutionFilePath);
            }
            catch (Exception ex)
            {

                MostrarMensaje(ex.Message);
            }
        }
        protected void gvDoctores_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDoctores.EditIndex = -1;
            LlenargvDoctores();
            LlenarchkTurno();
            LlenarrbIntervencion();
            LlenarddlSexo();
        }
        protected void gvDoctores_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                EntDoctor ent = new EntDoctor();
                ent.Id_Doctor = Convert.ToInt32(gvDoctores.DataKeys[e.RowIndex].Values["Id_Doctor"]);
                ent.Nombre_Doctor = ((TextBox)gvDoctores.Rows[e.RowIndex].FindControl("txtNombreEIT")).Text;
                ent.Paterno_Doctor = ((TextBox)gvDoctores.Rows[e.RowIndex].FindControl("txtPaternoEIT")).Text;
                ent.Materno_Doctor = ((TextBox)gvDoctores.Rows[e.RowIndex].FindControl("txtMaternoEIT")).Text;
                ent.Fecha_Naci_Doctor = Convert.ToDateTime(((TextBox)gvDoctores.Rows[e.RowIndex].FindControl("txtFechaEIT")).Text);
                ent.Id_Sexo_Doctor = Convert.ToInt32(((DropDownList)gvDoctores.Rows[e.RowIndex].FindControl("ddlSexoEIT")).SelectedValue);
                ent.Id_Intervencion_Doctor = Convert.ToInt32(((RadioButtonList)gvDoctores.Rows[e.RowIndex].FindControl("rbInterEIT")).SelectedValue);
                ent.Id_Turno_Doctor = Convert.ToInt32(((CheckBoxList)gvDoctores.Rows[e.RowIndex].FindControl("chkTurnoEIT")).SelectedValue);
                ent.Telefono_Doctor = ((TextBox)gvDoctores.Rows[e.RowIndex].FindControl("txtTeleEIT")).Text;
                BusDoctor obj = new BusDoctor();
                obj.Actualizar(ent);
                Response.Redirect(Request.CurrentExecutionFilePath);
            }
            catch (Exception ex)
            {

                MostrarMensaje(ex.Message);
            }
        }



        #endregion gvDoctores
        private void MostrarMensaje(string p)
        {
            string alerta = string.Format("alert('{0}')", p.Replace("'", "").Replace("\r", "").Replace("\n", ""));
            ScriptManager.RegisterStartupScript(this, GetType(), "", alerta, true);
        }
        protected void lnkAdd_Click(object sender, EventArgs e)
        {
            try
            {
                EntDoctor ent = new EntDoctor();
                ent.Nombre_Doctor = ((TextBox)gvDoctores.FooterRow.FindControl("txtNombreFT")).Text;
                ent.Paterno_Doctor = ((TextBox)gvDoctores.FooterRow.FindControl("txtPaternoFT")).Text;
                ent.Materno_Doctor = ((TextBox)gvDoctores.FooterRow.FindControl("txtMaternoFT")).Text;
                ent.Fecha_Naci_Doctor = Convert.ToDateTime(((TextBox)gvDoctores.FooterRow.FindControl("txtFechaFT")).Text);
                ent.Id_Sexo_Doctor = Convert.ToInt32(((DropDownList)gvDoctores.FooterRow.FindControl("ddlSexoFT")).SelectedValue);
                ent.Id_Intervencion_Doctor = Convert.ToInt32(((RadioButtonList)gvDoctores.FooterRow.FindControl("rbInterFT")).SelectedValue);
                ent.Id_Turno_Doctor = Convert.ToInt32(((CheckBoxList)gvDoctores.FooterRow.FindControl("chkTurnoFT")).SelectedValue);
                ent.Telefono_Doctor = ((TextBox)gvDoctores.FooterRow.FindControl("txtTeleFT")).Text;
                BusDoctor obj = new BusDoctor();
                obj.Insertar(ent);
                Response.Redirect(Request.CurrentExecutionFilePath);
            }
            catch (Exception ex)
            {
                
                MostrarMensaje(ex.Message);
            }
        }
        protected void gvDoctores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDoctores.SelectedIndex = -1;
            gvDoctores.PageIndex = e.NewPageIndex;
            LlenargvDoctores();                                                                         
        }
        protected void gvDoctores_Sorting(object sender, GridViewSortEventArgs e)
        {
            string columna = e.SortExpression;
            string direccion = "";
            if (ViewState["orden"] == null)
            {
                direccion = "asc";
                ViewState["orden"] = "desc";
            }
            else
            {
                direccion = ViewState["orden"].ToString();
                if (direccion == "asc")
                    ViewState["orden"] = "desc";
                else
                    ViewState["orden"] = "asc";
            }
            gvDoctores.DataSource = new BusDoctor().Ordenado(columna, direccion);
            gvDoctores.DataBind();
        }
}
    
