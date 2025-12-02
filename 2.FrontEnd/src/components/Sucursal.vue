<template>
    <v-layout align-start>
        <v-flex>

            <!-- Encabezado -->
            <v-toolbar flat color="white">
                <v-toolbar-title class="primary--text">Sucursal</v-toolbar-title>
                    <v-divider
                    class="mx-2"
                    inset
                    vertical
                    ></v-divider>

                    <v-spacer></v-spacer>

                    <!-- Busqueda -->
                    <v-text-field class="text-xs-center" v-model="search" append-icon="search" label="Búsqueda" single-line hide-details></v-text-field>
                    <v-spacer></v-spacer> 
                        
                    <!-- Boton Nuevo -->

                         <!-- Ventana Nuevo o Actualizar -->

                    <v-dialog v-model="dialog" max-width="500px">

                         <template v-slot:activator="{ on }"> 
                             <v-btn color="primary" dark class="mb-2" v-on="on">Nuevo</v-btn>                              
                         </template>

                            <!-- Ventana Nuevo o Actualizar -->
                            <v-card>
                                    <!-- Titulo de la ventana(NUEVO O ACTUALIZAR) -->
                                    <v-card-title>
                                    <span class="headline">{{ formTitle }}</span>
                                    </v-card-title>
                                
                                    <v-card-text>
                                        <v-container grid-list-md>
                                            <v-layout wrap>

                                            <v-flex xs12 sm12 md12>
                                                <v-text-field v-model="nombre" label="Nombre Sucursal"></v-text-field>
                                            </v-flex>

                                            <v-flex xs12 sm12 md12>
                                                <v-text-field v-model="codigo" label="Código"></v-text-field>
                                            </v-flex>

                                            <v-flex xs12 sm12 md12>
                                                <v-text-field v-model="direccion" label="Dirección"></v-text-field>
                                            </v-flex> 

                                            <v-flex xs12 sm12 md12>
                                                <v-text-field v-model="telefono" label="Teléfono"></v-text-field>
                                            </v-flex> 

                                            <v-flex xs12 sm12 md12>
                                                <v-text-field v-model="email" label="Correo"></v-text-field>
                                            </v-flex>

                                            <!-- Muestra msj de validacion de datos -->
                                            <v-flex xs12 sm12 md12 v-show="valida">
                                                <div class="red--text" v-for="v in validaMensaje" :key="v" v-text="v">
                                                </div>
                                            </v-flex>   

                                            </v-layout>
                                        </v-container>
                                    </v-card-text>

                                     <!-- Opcion Cancelar Guardar -->       
                                <v-card-actions>
                                    <v-spacer></v-spacer>
                                    <v-btn color="primary" text @click.native="close">Cancelar</v-btn>
                                    <!--llama al metodo guardar -->
                                    <v-btn color="primary" text @click.native="guardar">Guardar</v-btn>
                                </v-card-actions>

                            </v-card>
                      
                    </v-dialog>

                    <!--Ventana Activar o Desactivar -->
                    <v-dialog v-model="adModal" max-width="290">
                        <v-card>
                            <v-card-title class="headline" v-if="adAccion==1">¿Activar Item?</v-card-title>
                            <v-card-title class="headline" v-if="adAccion==2">¿Desactivar Item?</v-card-title>
                            <v-card-text>
                                Estás a punto de 
                                <span v-if="adAccion==1">Activar </span>
                                <span v-if="adAccion==2">Desactivar </span>
                                el ítem {{ adNombre }}
                            </v-card-text>
                            <v-card-actions>
                                <v-spacer></v-spacer>
                                <v-btn color="green darken-1" text="text" @click="activarDesactivarCerrar">
                                    Cancelar
                                </v-btn>
                                <v-btn v-if="adAccion==1" color="orange darken-4" text="text" @click="activar">
                                    Activar
                                </v-btn>
                                <v-btn v-if="adAccion==2" color="orange darken-4" text="text" @click="desactivar">
                                    Desactivar
                                </v-btn>
                            </v-card-actions>

                        </v-card>
                    </v-dialog>

            </v-toolbar>
                

            <!-- DataTable -->
            <v-data-table
                :headers="headers"
                :items="sucursales"
                :search="search"
                class="elevation-1"
            >
              
                <template v-slot:item="props">
                    <tr>
                        <td class="justify-center layout px-0">
                                
                             <!-- Icono Editar o Actualizar -->
                            <v-icon
                                small
                                class="mr-2"
                                @click="editItem(props.item)" >
                                    edit
                            </v-icon>
                                
                            <!-- Icono Activar Si Esta Desactivada -->
                            <template v-if="props.item.condicion">
                                <v-icon
                                    small
                                    @click="activarDesactivarMostrar(2,props.item)"
                                    >
                                    block
                                </v-icon>
                            </template>

                            <!-- Icono Desactivar Si activada -->
                            <template v-else>
                                <v-icon
                                    small
                                    @click="activarDesactivarMostrar(1,props.item)"
                                    >
                                    check
                                </v-icon>
                            </template>
                                

                            <!-- Columnas del DataTable (Nombre Descripcion) -->  
                            </td>

                                <td>{{ props.item.nombre}}</td>                                                             
                                <td>{{ props.item.codigo }}</td>  
                                <td>{{ props.item.direccion}}</td>                                                             
                                <td>{{ props.item.telefono }}</td>
                                <td>{{ props.item.email}}</td>                                                             
                                
                            <td >

                            <!-- Columnas del DataTable Mostrar ACTIVO -->  
                            <div v-if="props.item.condicion">
                                <span class="blue--text">Activo</span>
                            </div>

                            <!-- Columnas del DataTable Mostrar INACTIVO -->
                            <div v-else>
                                <span class="red--text">Inactivo</span>
                            </div>
                        </td>
                    </tr>  
                                    
                </template>

                
                    <!-- Opcion resetear si no hay Datos -->           
                <template v-slot:no-data>
                        <v-btn color="primary" @click="listar">Resetear</v-btn>
                </template>
            </v-data-table>
    
        </v-flex>
    </v-layout>
</template>



<!-- Logica --> 

<script>

    //import axios 
    import axios from 'axios'


    export default {

           //  Datos
        data(){

            return {
               sucursales:[],                            
               dialog: false,
               
               headers: [
                    { text: 'Opciones', value: 'actions', sortable: false },                    
                    { text: 'Nombre', value: 'nombre' },
                    { text: 'Código', value: 'codigo', sortable: false  },
                    { text: 'Dirección', value: 'direccion', sortable: false  },
                    { text: 'Telefono', value: 'email', sortable: false  },
                    { text: 'Correo', value: 'email', sortable: false  },
                    { text: 'Estado', value: 'condicion', sortable: false  }                
                ],
                
                search: '',
                editedIndex: -1,

                // variables de Presentacion
                id: '',
                nombre: '',
                codigo: '',
                direccion: '',
                telefono: '',
                email: '',
                
                descripcion: '',



                valida: 0,       //validar los datos
                validaMensaje:[], //validar el msj

                adModal: 0,
                adAccion: 0,
                adNombre: '',
                adNombres: '',
                adId: ''             
            }


        },




        computed: {
            //evaluar si la variable es 1 o -1 para nuevo y actulizar
            formTitle () {
                return this.editedIndex === -1 ? 'Nueva Sucursal' : 'Actualizar Sucursal'
            }
        },

        watch: {
            dialog (val) {
            val || this.close()
            }
        },

        created () {
            this.listar();
        },

        //---METODOS----------------------
        methods:{

            //Listar
            listar(){
                let me=this;

                //autorizacion
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};

                axios.get('api/Sucursales/Listar', configuracion).then(function(response){
                    //console.log(response);
                    me.sucursales=response.data;
                }).catch(function(error){
                    console.log(error);
                });
            },

            
          
            //Editar 
            editItem (item) {
                this.id=  item.idSucursal; //valores del DB               
                this.nombre=item.nombre;
                this.codigo=item.codigo;
                this.direccion=item.direccion;
                this.telefono=item.telefono;
                this.email=item.email;
                this.editedIndex=1;
                this.dialog = true
                this.valida= 0;
            },

            deleteItem (item) {
                const index = this.desserts.indexOf(item)
                confirm('Are you sure you want to delete this item?') && this.desserts.splice(index, 1)
            },

            //metodo cerrar
            close () {
                this.dialog = false;
                this.limpiar();
            },

            //metodo limpiar
            limpiar(){
                this.id="";
                this.nombre="";
                this.codigo="";
                this.direccion="";
                this.telefono="";
                this.email="";
                this.editedIndex=-1;
            },

            //metodo guardar
            guardar () {
                if (this.validar()){
                    return;
                }

                //autorizacion
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};

                if (this.editedIndex > -1) {

                    //Código para editar
                    
                    let me=this;
                    //Peticion ajax mediante axios
                    axios.put('/api/Sucursales/Actualizar',{
                        'idSucursal':me.id,
                        'nombre': me.nombre,
                        'codigo': me.codigo,
                        'direccion': me.direccion,
                        'telefono': me.telefono,

                        'email': me.email

                    },configuracion).then(function(response){
                        //si todo funciona correctamente llama al metodo response
                        me.close();
                        me.listar();
                        me.limpiar();                        
                    }).catch(function(error){
                        console.log(error);
                    });

                } else {
                    //Código para guardar
                    let me=this;
                    axios.post('/api/Sucursales/Crear',{
                        'Nombre': me.nombre,  
                        'Codigo': me.codigo,
                        'Direccion': me.direccion,
                        'Telefono': me.telefono,

                        'Email': me.email

                        
                    },configuracion).then(function(response){
                        me.close();
                        me.listar();
                        me.limpiar();                        
                    }).catch(function(error){
                        console.log(error);
                    });
                }
            },

            //metodo validar
            validar(){
                this.valida=0;
                this.validaMensaje=[];

                if (this.nombre.length<1 || this.nombre.length>100){
                    this.validaMensaje.push("Favor llenar el campo Nombre Sucursal");
                }

                if (this.validaMensaje.length){
                    this.valida=1;
                }
                return this.valida;
            },


            //metodo Activar o desactivar
            activarDesactivarMostrar(accion,item){
                this.adModal=1; 
                this.adNombre=item.nombre; //valor de la BD
                this.adId=item.idSucursal;  //valor de la BD              
                if (accion==1){
                    this.adAccion=1;
                }
                else if (accion==2){
                    this.adAccion=2;
                }
                else{
                    this.adModal=0;
                }
            },
            activarDesactivarCerrar(){
                this.adModal=0;
            },
            activar(){
                let me=this;

                //autorizacion
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};

                axios.put('/api/Sucursales/Activar/'+this.adId,{},configuracion).then(function(response){
                    me.adModal=0;
                    me.adAccion=0;
                  
                    me.adId="";
                    me.listar();                       
                }).catch(function(error){
                    console.log(error);
                });
            },
            desactivar(){
                let me=this;
                //autorizacion
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                axios.put('/api/Sucursales/Desactivar/'+this.adId,{},configuracion).then(function(response){
                    me.adModal=0;
                    me.adAccion=0;                 
                    me.adId="";
                    me.listar();                       
                }).catch(function(error){
                    console.log(error);
                });
            }


        }        
    }






</script>




