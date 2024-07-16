using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ArticuloDatos
    {
        List<Articulo> listado = new List<Articulo>();
        AccesoDatos datos = new AccesoDatos();
        private void crearListado()
        {
            while (datos.Lector.Read())
            {
                Articulo articulo = new Articulo();

                articulo.Id = (int)datos.Lector["Id"];
                articulo.Codigo = datos.Lector.IsDBNull(datos.Lector.GetOrdinal("Codigo")) ? null : (string)datos.Lector["Codigo"];
                articulo.Nombre = datos.Lector.IsDBNull(datos.Lector.GetOrdinal("Nombre")) ? null : (string)datos.Lector["Nombre"];
                articulo.Descripcion = datos.Lector.IsDBNull(datos.Lector.GetOrdinal("Descripcion")) ? null : (string)datos.Lector["Descripcion"];

                articulo.Marca = new Marca();
                articulo.Marca.Id = (int)datos.Lector["IdMarca"];
                articulo.Marca.Nombre = datos.Lector.IsDBNull(datos.Lector.GetOrdinal("Marca")) ? null : (string)datos.Lector["Marca"];

                articulo.Categoria = new Categoria();
                articulo.Categoria.Id = (int)datos.Lector["IdCategoria"];
                articulo.Categoria.Descripcion = datos.Lector.IsDBNull(datos.Lector.GetOrdinal("Categoria")) ? null : (string)datos.Lector["Categoria"];

                articulo.UrlImagen = datos.Lector.IsDBNull(datos.Lector.GetOrdinal("ImagenUrl")) ? null : (string)datos.Lector["ImagenUrl"];

                articulo.Precio = datos.Lector.IsDBNull(datos.Lector.GetOrdinal("Precio")) ? 0 : (decimal)datos.Lector["Precio"];


                listado.Add(articulo);

            }
            
        }
        public List<Articulo> listar()
        {
            
            try
            {
                datos.setearConsulta("select A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion Marca, C.Descripcion Categoria, ImagenUrl, Precio, A.IdCategoria, A.IdMarca from ARTICULOS A, CATEGORIAS C, MARCAS M where A.IdMarca= M.Id and A.IdCategoria=C.Id");
                datos.ejecutarLectura();

                crearListado();
                return listado;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {

                datos.cerrarConexion();
            }
        }

        public void agregarArticulo(Articulo nuevoArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) values (@CodigoArt, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @ImgUrl, @Precio)");
                datos.setearParametros("@CodigoArt", nuevoArticulo.Codigo);
                datos.setearParametros("@Nombre", nuevoArticulo.Nombre);
                datos.setearParametros("@Descripcion", nuevoArticulo.Descripcion);
                datos.setearParametros("@IdMarca", nuevoArticulo.Marca.Id);
                datos.setearParametros("@IdCategoria", nuevoArticulo.Categoria.Id);
                datos.setearParametros("@ImgUrl", nuevoArticulo.UrlImagen);
                datos.setearParametros("@Precio", nuevoArticulo.Precio);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }

        public void eliminarArticulo(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("delete from ARTICULOS where Id=@id");
                datos.setearParametros("@id",id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void modificarArticulo(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo=@codigo, Nombre=@nombre, Descripcion=@descripcion, IdMarca=@IdMarca, IdCategoria=@IdCat, ImagenUrl=@UrlImg, Precio=@precio where Id=@id");
                datos.setearParametros("@codigo", articulo.Codigo);
                datos.setearParametros("@nombre", articulo.Nombre);
                datos.setearParametros("@descripcion", articulo.Descripcion);
                datos.setearParametros("@IdMarca", articulo.Marca.Id);
                datos.setearParametros("@IdCat", articulo.Categoria.Id);
                datos.setearParametros("@UrlImg", articulo.UrlImagen);
                datos.setearParametros("@precio", articulo.Precio);
                datos.setearParametros("@id", articulo.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }

        public List<Articulo> filtrar(string criterio, string subcriterio, string buscado)
        {

            try
            {
                string consulta = "select A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion Marca, C.Descripcion Categoria, ImagenUrl, Precio, A.IdCategoria, A.IdMarca from ARTICULOS A, CATEGORIAS C, MARCAS M where A.IdMarca= M.Id and A.IdCategoria=C.Id and";
                switch (criterio)
                {
                    case "Precio":
                        switch (subcriterio)
                        {
                            case "Mayor a":
                                consulta += " Precio > " + buscado;
                                break;
                            case "Menor a":
                                consulta += " Precio < " + buscado;
                                break;
                            default:
                                consulta += " Precio = " + buscado;
                                break;
                        }
                    break;
                    case "Marca":
                        consulta += " M.Descripcion like '" + subcriterio + "' and Nombre like '%" + buscado + "%'";
                        break;
                    case "Categoría":
                        consulta += " C.Descripcion like '" + subcriterio + "' and Nombre like '%" + buscado + "%'";
                        break;

                    default:
                        break;

                    
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                crearListado();
                return listado;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}