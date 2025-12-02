import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'

import Inicio from '../components/Inicio.vue'
import Categoria from '../components/Categoria.vue'
import Impuesto from '../components/Impuesto.vue'
import Marca from '../components/Marca.vue'
import Articulo from '../components/Articulo.vue'

import Comprobante from '../components/Comprobante.vue'

import Rol from '../components/Rol.vue'
import Usuario from '../components/Usuario.vue'

import Cliente from '../components/Cliente.vue'
import Proveedor from '../components/Proveedor.vue'

import Sucursal from '../components/Sucursal.vue'
import Caja from '../components/Caja.vue'

import Login from '../components/Login.vue'
import store from '../store/index.js'

import Ingreso from '../components/Ingreso.vue'

import Venta from '../components/Venta.vue'

import ConsultaVenta from '../components/ConsultaVenta.vue'
import ConsultaIngreso from '../components/ConsultaIngreso.vue'
import ConsultaArticulo from '../components/ConsultaArticulo.vue'

import CategoriaPrincipal from '../components/CategoriaPrincipal.vue'
import CategoriaSecundaria from '../components/CategoriaSecundaria.vue'

import BitacoraEnvio from '../components/BitacoraEnvio.vue'
import CertificadoDigital from '../components/CertificadoDigital.vue'




Vue.use(VueRouter)



var router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes:

  [
 
   {
      path: '/',
      name: 'home',
      component: Home,
      meta :{
        administrador :true,
        almacen: true,
        vendedor: true
      }
    },

    //Categorias
    {
      path: '/categorias',
      name: 'categorias',
      component: Categoria,
      meta :{
        administrador :true,
        almacen: true
      }
    },

    //Marcas
    {
      path: '/marcas',
      name: 'marcas',
      component: Marca,
      meta :{
        administrador :true,
        almacen: true
      }
    },

    //Impuestos
    {
      path: '/impuestos',
      name: 'impuestos',
      component: Impuesto,
      meta :{
        administrador :true     
      }
    },

    //Comprobantes
    {
      path: '/comprobantes',
      name: 'comprobantes',
      component: Comprobante,
        meta :{
          administrador :true,
          vendedor: true
        }
    },
    
    //Articulos
    {
    path: '/articulos',
    name: 'articulos',
    component: Articulo,
    meta :{
      administrador :true,
      almacen: true
    }
    },

    //Inicio
    {
    path: '/inicio',
    name: 'inicio',
    component: Inicio,meta :{
      administrador :true,
      almacen: true,
      vendedor: true
      }
  },

  //Roles
  {
    path: '/roles',
    name: 'roles',
    component: Rol,
    meta :{
      administrador :true
    }
  },

  //Usuarios
  {
    path: '/usuarios',
    name: 'usuarios',
      component: Usuario,
      meta :{
        administrador :true
      }
  },
  {
    path: '/clientes',
    name: 'clientes',
    component: Cliente,
    meta :{
      administrador :true,
      vendedor: true
    }
  },
  {
    path: '/proveedores',
    name: 'proveedores',
    component: Proveedor,
    meta :{
      administrador :true,
      almacen: true
    }
  },

  {
    path: '/sucursales',
    name: 'sucursales',
    component: Sucursal,
    meta :{
      administrador :true,
      almacen: true
    }
  },

  {
    path: '/cajas',
    name: 'cajas',
    component: Caja,
    meta :{
      administrador :true,
      almacen: true
    }
  },
  //Ingreso
  {
    path: '/ingresos',
    name: 'ingresos',
    component: Ingreso,
    meta :{
      administrador :true,
      almacen: true
    }
  },

  //Venta
  {
    path: '/ventas',
    name: 'ventas',
    component: Venta,
      meta :{
        administrador :true,
        vendedor: true
      }
  },
   //Venta
   {
    path: '/consultaventas',
    name: 'consultaventas',
    component: ConsultaVenta,
      meta :{
        administrador :true,
       
      }
  },
  //Consulta Ingreso
  {
    path: '/consultaingresos',
    name: 'consultaingresos',
    component: ConsultaIngreso,
      meta :{
        administrador :true,
       
      }
  },
  //Consulta Articulo
  {
    path: '/consultaarticulos',
    name: 'consultaarticulos',
    component: ConsultaArticulo,
      meta :{
        administrador :true,
       
      }
  },

   //CategoriaPrincipal
   {
    path: '/categoriaprincipal',
    name: 'categoriaprincipal',
    component: CategoriaPrincipal,
      meta :{
        administrador :true,
        almacen: true
       
      }
  },

  //CategoriaSecundaria
  {
    path: '/categoriasecundaria',
    name: 'categoriasecundaria',
    component: CategoriaSecundaria,
      meta :{
        administrador :true,
        almacen: true
       
      }
  },
  //Bitacora Envio
  {
    path: '/bitacoraenvios',
    name: 'bitacoraenvios',
    component: BitacoraEnvio,
      meta :{
        administrador :true,
        vendedor: true
       
      }
  },
  //Certificado Digital
  {
    path: '/certificadodigitales',
    name: 'certificadodigitales',
    component: CertificadoDigital,
      meta :{
        administrador :true,
        vendedor: true
       
      }
  },

  {
    path: '/login',
    name: 'login',
    component: Login,
    meta : {
      libre: true
    }
  }

  ]
})



router.beforeEach((to, from, next) => {
  if (to.matched.some(record => record.meta.libre)){
    next()
  } else if (store.state.usuario && store.state.usuario.rol== 'Administrador'){
    if (to.matched.some(record => record.meta.administrador)){
      next()
    }
  }else if (store.state.usuario && store.state.usuario.rol== 'Almacen'){
    if (to.matched.some(record => record.meta.almacen)){
      next()
    }
  }else if (store.state.usuario && store.state.usuario.rol== 'Vendedor'){
    if (to.matched.some(record => record.meta.vendedor)){
      next()
    }
  } else{
    next({
      name: 'login'
    })
  }
})

export default router
