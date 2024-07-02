using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace Presentacion
{
    public partial class frmPrincipal : Form
    {
        private List<Articulo> listaArticulos;
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void btnBusquedaAvanzada_Click(object sender, EventArgs e)
        {
            frmBusquedaAvanzada busqueda = new frmBusquedaAvanzada();
            busqueda.ShowDialog();
        }

        private void bntAgregar_Click(object sender, EventArgs e)
        {
            frmAgregar agregar = new frmAgregar();
            agregar.ShowDialog();
        }

        private void cargarImagen(string url)
        {
            try
            {
                pbxArticulo.Load(url);

            }
            catch (Exception ex)
            {

                pbxArticulo.Load("https://archive.org/download/placeholder-image/placeholder-image.jpg");
            }
        }
        private void cargarDescripcion(string descripcion)
        {
            try
            {
                txtDescripcionArt.Text = descripcion;
            }
            catch (Exception ex)
            {
                descripcion = "Descripción no disponible.";
                txtDescripcionArt.Text = descripcion;
            }
        }
        private void frmPrincipal_Load(object sender, EventArgs e)
        {

            ArticuloDatos articulos = new ArticuloDatos();
            listaArticulos = articulos.listar();
            dgvArticulos.DataSource = listaArticulos;

            cargarImagen(listaArticulos[0].UrlImagen);
            cargarDescripcion(listaArticulos[0].Descripcion);
            

            dgvArticulos.Columns["Id"].Visible = false;
            dgvArticulos.Columns["UrlImagen"].Visible = false;
            dgvArticulos.Columns["Descripcion"].Visible = false;
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            Articulo seleccion = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            cargarImagen(seleccion.UrlImagen);
            cargarDescripcion(seleccion.Descripcion);
        }
    }
}
