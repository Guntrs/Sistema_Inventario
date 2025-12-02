<template>
  <div id="app">
    <v-app>

        <!-- menu -->
      <v-navigation-drawer 
          
          v-if="logueado" v-model="drawer"
            :clipped="$vuetify.breakpoint.mdAndUp" 
             app>
             
        <v-list dense >
          
            <!-- Inicio -->
          <template v-if="esAdministrador || esAlmacen || esVendedor">          
            <v-list-item :to="{ name: 'inicio'}">
              <v-list-item-action>
                <v-icon color="primary">home</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title class="primary--text ">
                  Inicio
                </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </template> 

            <!-- Mantenimientos -->
          <template v-if="esAdministrador || esAlmacen" >
            
            <v-list-group color="blue"   >
              
              <v-list-item slot="activator">
                <v-list-item-content>
                  <v-list-item-title class="primary--text " > 
                    <v-icon color="primary">gavel</v-icon>                    
                   
                    Mantenimientos
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>

               <!-- Categoria Principal -->
               <v-list-item :to="{ name: 'categoriaprincipal'}">
                <v-list-item-action>
                
                </v-list-item-action>
                <v-list-item-content>
                  <v-list-item-title class="dark--text ">
                    Categoría Principal
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>

              <!-- Categoria Secundaria -->
              <v-list-item :to="{ name: 'categoriasecundaria'}">
                <v-list-item-action>
                
                </v-list-item-action>
                <v-list-item-content>
                  <v-list-item-title class="dark--text ">
                    Categoría Secundaria
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>

                <!-- Categorias -->
              <v-list-item :to="{ name: 'categorias'}">
                <v-list-item-action>
                
                </v-list-item-action>
                <v-list-item-content>
                  <v-list-item-title class="dark--text ">
                    Categorías
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>
              
              <v-list-item :to="{ name: 'articulos'}">
                <v-list-item-action>
               
                </v-list-item-action>
                <v-list-item-content>
                  <v-list-item-title>
                    Artículos
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>

              <v-list-item :to="{ name: 'impuestos'}">
                <v-list-item-action>
                 
                </v-list-item-action>
                <v-list-item-content>
                  <v-list-item-title>
                    Impuesto
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>

              <v-list-item :to="{ name: 'comprobantes'}">
              <v-list-item-action>
             
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>
                  Tipo Comprobante
                </v-list-item-title>
              </v-list-item-content>
              </v-list-item>

              <v-list-item :to="{ name: 'marcas'}">
                <v-list-item-action>
               
                </v-list-item-action>
                <v-list-item-content>
                  <v-list-item-title>
                    Marca
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>     
            </v-list-group>
          </template>

          <!-- Compras -->
          <template v-if="esAdministrador || esAlmacen">
            <v-list-group color="blue">
              <v-list-item slot="activator">
                <v-list-item-content>
                  <v-list-item-title class="primary--text ">
                   
                    <v-icon color="primary">widgets</v-icon>
                    Compras
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>


              <v-list-item :to="{ name: 'ingresos'}">
                <v-list-item-action>
                 
                </v-list-item-action>
                <v-list-item-content>
                  <v-list-item-title>
                    Ingresos
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>

              
              <v-list-item :to="{ name: 'proveedores'}">
                <v-list-item-action>
                  
                </v-list-item-action>
                <v-list-item-content>
                  <v-list-item-title>
                    Proveedores
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>

            </v-list-group>
          </template>

          <!-- Ventas -->
          <template v-if="esAdministrador|| esVendedor">
            <v-list-group color="blue">
              
              <v-list-item slot="activator">
                <v-list-item-content>
                  <v-list-item-title class="primary--text ">
                    <v-icon color="primary">folder_open</v-icon>
                    Ventas
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>


              <v-list-item :to="{ name: 'ventas'}">
                <v-list-item-action>
                  
                </v-list-item-action>
                <v-list-item-content>
                  <v-list-item-title>
                    Ventas
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>

              <v-list-item :to="{ name: 'clientes'}">
                <v-list-item-action>
                 
                </v-list-item-action>
                <v-list-item-content>
                  <v-list-item-title>
                    Clientes
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>

            </v-list-group>
          </template>

          <!-- Accesos -->
          <template v-if="esAdministrador">
            <v-list-group color="blue">
              <v-list-item slot="activator">
                <v-list-item-content>
                  <v-list-item-title class="primary--text " >
                    <v-icon color="primary">lock</v-icon>
                    Accesos
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>
              <v-list-item :to="{ name: 'roles'}">
                <v-list-item-action>
                  
                </v-list-item-action>
                <v-list-item-content>
                  <v-list-item-title>
                    Roles
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>
              <v-list-item :to="{ name: 'usuarios'}">
                <v-list-item-action>
                  
                </v-list-item-action>
                <v-list-item-content>
                  <v-list-item-title>
                    Usuarios
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>
            </v-list-group>
          </template>

            <!-- Sucursales -->
          <template v-if="esAdministrador">
            <v-list-group color="blue">
              <v-list-item slot="activator">
                <v-list-item-content>
                  <v-list-item-title  class="primary--text ">
                    <v-icon color="primary">business</v-icon>
                    Sucursales
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>
              <v-list-item :to="{ name: 'sucursales'}">
                <v-list-item-action>
                 
                </v-list-item-action>
                <v-list-item-content>
                  <v-list-item-title>
                    Sucursal
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>
              <v-list-item :to="{ name: 'cajas'}">
                <v-list-item-action>
                  
                </v-list-item-action>
                <v-list-item-content>
                  <v-list-item-title>
                    Cajas
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>
            </v-list-group>
          </template>
          
           <!-- Factura -->
           <template v-if="esAdministrador">
            <v-list-group color="blue">
              <v-list-item slot="activator">
                <v-list-item-content>
                  <v-list-item-title  class="primary--text ">
                    <v-icon color="primary">dialpad</v-icon>
                    Factura
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>

              <v-list-item :to="{ name: 'bitacoraenvios'}">
                <v-list-item-action>                
                </v-list-item-action>
                <v-list-item-content>
                  <v-list-item-title>
                    Bitacora Envio
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>

              <v-list-item :to="{ name: 'certificadodigitales'}">
                <v-list-item-action>                 
                </v-list-item-action>
                <v-list-item-content>
                  <v-list-item-title>
                    Certificado Digital
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>
            </v-list-group>

          </template>

          <!-- Consultas -->
          <template v-if="esAdministrador">
            <v-list-group color="blue">
              <v-list-item slot="activator">
                <v-list-item-content>
                  <v-list-item-title class="primary--text ">
                    <v-icon color="primary">event</v-icon>
                    Consultas
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>
              <v-list-item :to="{ name: 'consultaingresos'}">
                <v-list-item-action>
               
                
              
                  </v-list-item-action>
                <v-list-item-content>
                  <v-list-item-title>
                    Consultas Compras
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>
              <v-list-item :to="{ name: 'consultaventas'}">
                <v-list-item-action>
                  
                </v-list-item-action>
                <v-list-item-content>
                  <v-list-item-title>
                    Consultas ventas
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>

            </v-list-group>
          </template>
      
        </v-list>
      </v-navigation-drawer>

       <!-- Barra  -->
      <v-app-bar :clipped-left="$vuetify.breakpoint.mdAndUp" app color="primary" dark >


        <v-toolbar-title style="width: 300px" v-if="logueado"  class="ml-0 pl-3">
          <v-app-bar-nav-icon @click.stop="drawer = !drawer">    
          </v-app-bar-nav-icon>
          <router-link tag="span" class="hidden-sm-and-down" to="/">
            Sistema Nova 
          </router-link>
        </v-toolbar-title>

        <v-toolbar-title 
        style="width: 300px"  
        class="ml-0 pl-3" v-if="logueado" icon v-text=this.$store.state.usuario.nombre>       
        </v-toolbar-title>

        <v-spacer>
        </v-spacer>

        <!-- salir  -->
        <v-btn @click="salir" v-if="logueado" icon>
          <v-icon>logout</v-icon> Salir
        </v-btn>

        <!-- login -->
        <v-btn :to="{name: 'login'}" v-else color="primary">
          <v-icon>apps</v-icon> Login
        </v-btn>


        <v-btn icon large>
          <v-avatar size="32px" item >
            <v-img
              src="assets/logo.svg"
              alt="Vuetify"
            >
            </v-img></v-avatar>
        </v-btn>

      </v-app-bar>


      
      <v-main>
        <v-container fluid fill-height >
          <v-row class="fill-height">
              <v-col cols="12" class="fill-height">
                <router-view></router-view>
              </v-col>
          </v-row>
        </v-container>
      </v-main>
    
      <!-- Pie de pagina -->
      <v-footer color="primary" padless>
        <v-col class="primary  py-4 text-center white--text" cols="12">
            St &copy;2024
        </v-col>   
      </v-footer>

    </v-app>
  </div>
</template>





<script>
export default {
  name: 'App',

  data: () => ({
    
      drawer: null,
    
  }),

   computed: {
    logueado(){
      return this.$store.state.usuario;
      
    },
    esAdministrador(){
      return this.$store.state.usuario && this.$store.state.usuario.rol =='Administrador';
    },
    esAlmacen(){
      return this.$store.state.usuario && this.$store.state.usuario.rol =='Almacen';
    },
    esVendedor(){
      return this.$store.state.usuario && this.$store.state.usuario.rol =='Vendedor';
    }
  },

   created(){
    this.$store.dispatch("autoLogin");
  },
  methods:{
    salir(){
      this.$store.dispatch("salir");
    }
  }


};

</script>