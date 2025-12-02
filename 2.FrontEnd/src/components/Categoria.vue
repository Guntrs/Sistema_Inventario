<template>
    <v-layout align-start>
        <v-flex>

            <!-- Encabezado -->
            <v-toolbar flat color="white">
                <v-toolbar-title class="primary--text">Categorías Nova</v-toolbar-title>
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

                                                <v-flex xs6 sm6 md6>
                                                    <v-select v-model="idsecundaria"
                                                        :items="categoriasecundarias"
                                                         label="Categoria Secundaria">
                                                     </v-select>
                                                </v-flex>

                                            <v-flex xs12 sm12 md12>
                                                <v-text-field v-model="nombre" 
                                                label="Nombre Categoria Articulo"></v-text-field>
                                            </v-flex>
                                            <v-flex xs12 sm12 md12>
                                                <v-text-field v-model="descripcion" label="Descripción"></v-text-field>
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
                :items="categorias"
                :search="search"
                class="elevation-1"
            >
              
                <template v-slot:item="props">
                    <tr>
                        <td class="justify-center layout px-0">
                                
                             <!-- Icono Editar o Actualizar -->
                            <v-icon
                                color="black"
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
                            <td >
                                    <strong >
                                        <div >
                                            <span class="green--text">
                                                {{ props.item.categoriaSecundaria }}
                                            </span>
                                        </div>
                                    </strong>                       
                                </td>
                                <td >
                                    <strong >
                                        <div >
                                            <span class="blue--text">
                                                {{ props.item.nombre }}
                                            </span>
                                        </div>
                                    </strong>                       
                                </td>
                               
                               
                                <td>{{ props.item.descripcion }}</td>  
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

           // 
        data(){

            return {
               categorias:[],                            
               dialog: false,
               
               headers: [
                    { text: 'Opciones', value: 'actions', sortable: false },  
                    { text: 'Categoria Secundaria', value: 'categoriasecundaria' },                 
                    { text: 'Categoria del Articulo', value: 'nombre' },
                    { text: 'Descripción', value: 'descripcion', sortable: false  },
                    { text: 'Estado', value: 'condicion', sortable: false  }                
                ],
                
                search: '',
                editedIndex: -1,

                // variables de Categoria
                id: '',
                nombre: '',
                descripcion: '',

                categoriasecundarias:[],
                idsecundaria:'',

                valida: 0,       //validar los datos
                validaMensaje:[], //validar el msj

                adModal: 0,
                adAccion: 0,
                adNombre: '',
                adId: ''             
            }


        },




        computed: {
            //evaluar si la variable es 1 o -1 para nuevo y actulizar
            formTitle () {
                return this.editedIndex === -1 ? 'Nueva Categoría' : 'Actualizar Categoría'
            }
        },

        watch: {
            dialog (val) {
            val || this.close()
            }
        },

        created () {
            this.listar();
            this.select5();
        },

        //---METODOS----------------------
        methods:{

            //Listas Categorias
            listar(){
                let me=this;
                //autorizacion
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};

                axios.get('api/Categorias/Listar',configuracion).then(function(response){
                    //console.log(response);
                    me.categorias=response.data;
                }).catch(function(error){
                    console.log(error);
                });
            },

            select5(){
                let me=this;  
                
                //para autorizacion  //configuracion
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};

                var categoriasArray=[];
                axios.get('api/CategoriaSecundarias/Select',configuracion).then(function(response){
                    categoriasArray=response.data;
                    categoriasArray.map(function(x){
                        me.categoriasecundarias.push({text: x.nombre, value:x.idCategoriaSecundaria},); //valores del Select BD
                    });
                }).catch(function(error){
                    console.log(error);
               });
            },
          
            //Editar Categorias
            editItem (item) {
                this.id=  item.idCategoria; //valores del DB
                this.idsecundaria=item.idCategoriaSecundaria;
                this.nombre=item.nombre;
                this.descripcion=item.descripcion;
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
                this.idsecundaria="";
                this.nombre="";
                this.descripcion="";
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
                    let me=this;
                    //Peticion ajax mediante axios
                    axios.put('/api/Categorias/Actualizar',{
                        'idcategoria':me.id,
                        'idcategoriasecundaria': me.idsecundaria,
                        'nombre': me.nombre,
                        'descripcion': me.descripcion

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
                    axios.post('/api/Categorias/Crear',{
                        'IdCategoriaSecundaria':me.idsecundaria,
                        'Nombre': me.nombre,
                        'Descripcion': me.descripcion
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


                if (this.validaMensaje.length){
                    this.valida=1;
                }
                return this.valida;
            },


            //metodo Activar o desactivar
            activarDesactivarMostrar(accion,item){
                this.adModal=1; 
                this.adNombre=item.nombre; //valor de la BD
                this.adId=item.idCategoria;  //valor de la BD              
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

                axios.put('/api/Categorias/Activar/'+this.adId,{},configuracion).then(function(response){
                    me.adModal=0;
                    me.adAccion=0;
                   // me.adNombre="";
                    me.adId="";
                    me.listar();                       
                }).catch(function(error){
                    console.log(error);
                });
            },
            desactivar(){
                let me=this;
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};

                axios.put('/api/Categorias/Desactivar/'+this.adId,{}, configuracion).then(function(response){
                    me.adModal=0;
                    me.adAccion=0;
                   // me.adNombre="";
                    me.adId="";
                    me.listar();                       
                }).catch(function(error){
                    console.log(error);
                });
            }
        }        
    }






</script>




