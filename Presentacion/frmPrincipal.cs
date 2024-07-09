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

        private void cargarGrilla()
        {
            ArticuloDatos articulos = new ArticuloDatos();
            try
            {
                listaArticulos = articulos.listar();
                dgvArticulos.DataSource = listaArticulos;
                dgvArticulos.ClearSelection();

                cargarImagen(listaArticulos[0].UrlImagen);
                cargarDescripcion(listaArticulos[0].Descripcion);
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void bntAgregar_Click(object sender, EventArgs e)
        {
            frmAgregar agregar = new frmAgregar();
            agregar.ShowDialog();
            cargarGrilla();
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

        private void ocultarColumnas()
        {
            dgvArticulos.Columns["Id"].Visible = false;
            dgvArticulos.Columns["UrlImagen"].Visible = false;
            dgvArticulos.Columns["Descripcion"].Visible = false;
        }
        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            cargarGrilla();
            ocultarColumnas();
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvArticulos.CurrentRow != null)
            {
                Articulo seleccion = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                cargarImagen(seleccion.UrlImagen);
                cargarDescripcion(seleccion.Descripcion);
            }
            
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo articuloSeleccionado;
            articuloSeleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

            frmAgregar modificacion = new frmAgregar(articuloSeleccionado);
            modificacion.ShowDialog();
            cargarGrilla();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloDatos articulo = new ArticuloDatos();
            Articulo seleccionado;
            try
            {
                DialogResult confirmacion=MessageBox.Show("¿Desea eliminar el artículo?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                
                if (confirmacion == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    articulo.eliminarArticulo(seleccionado.Id);
                    cargarGrilla();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            List<Articulo> busqueda;
            string filtro = txtBuscar.Text;

            if (filtro != "")
            {
                busqueda = listaArticulos.FindAll(x => x.Nombre.ToLower().Contains(filtro.ToLower()) || x.Marca.Nombre.ToLower().Contains(filtro.ToLower()) || x.Categoria.Descripcion.ToLower().Contains(filtro.ToLower()));

            }
            else {
                busqueda = listaArticulos;
            }

            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = busqueda;
            ocultarColumnas();
        }

        

        private void txtBuscar_MouseLeave(object sender, EventArgs e)
        {
            lblBusqueda.Text = "";
        }

        private void txtBuscar_MouseEnter(object sender, EventArgs e)
        {
            lblBusqueda.Text = "Ingrese nombre, marca o categoría del artículo.";
        }
    }
}
