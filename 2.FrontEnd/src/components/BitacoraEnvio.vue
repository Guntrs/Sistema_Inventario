<template>
    <v-layout align-start>
        <v-flex>

            <!-- Encabezado -->
            <v-toolbar flat color="white">
                <v-toolbar-title class="primary--text">Bitacora Envio</v-toolbar-title>
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
                            <v-card-title>
                            <span class="headline">{{ formTitle }}</span>
                            </v-card-title>
                
                            <v-card-text>
                            <v-container grid-list-md>
                                <v-layout wrap>

                                <v-flex xs6 sm6 md6>
                                            <v-select v-model="idsucursal"
                                            :items="sucursales" label="Sucursal">
                                            </v-select>
                                </v-flex>
    
                               
                                <v-flex xs12 sm12 md12>
                                    <v-text-field v-model="descripcion" label="Descripción"></v-text-field>
                                </v-flex>

                               
                                 

                                 
                                <v-flex xs12 sm12 md12 v-show="valida">
                                    <div class="red--text" v-for="v in validaMensaje" :key="v" v-text="v">
                                    </div>
                                </v-flex>   

                                </v-layout>
                            </v-container>
                            </v-card-text>
                


                            <v-card-actions>
                                <v-spacer></v-spacer>
                                <v-btn color="primary" text @click.native="close">Cancelar</v-btn>
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
                                el ítem {{ adDescripcion }}
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
                :items="cajas"
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
                                <td>{{ props.item.fechaEnvio }}</td>
                              
                                <td>{{ props.item.respuestaSAT }}</td> 
                                <td>{{ props.item.codigoRespuestaSAT }}</td> 
                                <td>{{ props.item.mensajeRespuestaSAT }}</td>  
                                
                                <td>{{ props.item.total }}</td> 
                                <td>{{ props.item.serieSAT }}</td> 
                                <td>{{ props.item.numeroSAT }}</td> 
                                <td>{{ props.item.estado }}</td> 
                                 
                            <td >

                           

                            
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
                cajas:[],                            
                dialog: false,
               
               headers: [
               {    text: 'Opciones', value: 'actions', sortable: false }, 
                                       
                    { text: 'Fecha Envio', value: 'fechaenvio', sortable: false  }, 
                    { text: 'Respuesta Sat', value: 'respuestasat', sortable: false  }, 
                    { text: 'Codigo Sat', value: 'codigosat', sortable: false  },
                    { text: 'Mensaje Sat', value: 'mensajesat', sortable: false  },
                    
                    { text: 'Total Factura', value: 'total', sortable: false  } ,
                    { text: 'Serie Sat', value: 'seriesat', sortable: false  } ,
                    { text: 'Numero Sat', value: 'numerosat', sortable: false  } , 
                    { text: 'Estado Factura', value: 'estado', sortable: false  } 
                                    
                ],
                
                search: '',
                editedIndex: -1,

                // variables de Impuesto
                id: '',

                sucursales:[],

                idsucursal:'',
                descripcion: '',
             

                

                valida: 0,       //validar los datos
                validaMensaje:[], //validar el msj

               



                adModal: 0,
                adAccion: 0,
                adDescripcion: '',
                adNombres: '',
                adId: ''             
            }


        },




        computed: {
            //evaluar si la variable es 1 o -1 para nuevo y actualizar
            formTitle () {
                return this.editedIndex === -1 ? 'Nueva Caja' : 'Actualizar Caja'
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
            //Listas Impuesto
            listar(){
                let me=this;
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                axios.get('/api/BitacoraEnvios/Listar', configuracion).then(function(response){
                    //console.log(response);
                    me.cajas=response.data;
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
                axios.get('api/Sucursales/Select',configuracion).then(function(response){
                    categoriasArray=response.data;
                    categoriasArray.map(function(x){
                        me.sucursales.push({text: x.nombre, value:x.idSucursal},); //valores del Select BD
                    });
                }).catch(function(error){
                    console.log(error);
               });
            },
                   
            //Editar Impuestos
            editItem (item) {
                this.id=item.idCaja; //valores del DB
                this.idsucursal=item.idSucursal;
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
                this.idsucursal="";
                this.descripcion="";
                this.editedIndex=-1;
            },

            guardar () {
                if (this.validar()){
                    return;
                }

                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};

                if (this.editedIndex > -1) {
                    //Código para editar
                    
                    let me=this;
                    axios.put('/api/Cajas/Actualizar',{
                        'idcaja':me.id,
                        'idsucursal': me.idsucursal,

                        'descripcion': me.descripcion
                        
                        
                    },configuracion).then(function(response){
                        me.close();
                        me.listar();
                        me.limpiar();                        
                    }).catch(function(error){
                        console.log(error);
                    });
                } else {
                    //Código para guardar
                    let me=this;
                    axios.post('/api/Cajas/Crear',{
                        

                        'idsucursal':me.idsucursal,

                        'descripcion': me.descripcion
                        
                    },configuracion).then(function(response){
                        me.close();
                        me.listar();
                        me.limpiar();                        
                    }).catch(function(error){
                        console.log(error);
                    });
                }
            },


            //validar
            validar(){
                this.valida=0;
                this.validaMensaje=[];

                if (this.descripcion.length<3 || this.descripcion.length>100){
                    this.validaMensaje.push("El Impuesto debe tener más de 3 caracteres y menos de 100 caracteres");
                }
                if (this.validaMensaje.length){
                    this.valida=1;
                }
                return this.valida;
            },

             //metodo Activar o desactivar
             activarDesactivarMostrar(accion,item){
                this.adModal=1; 
                this.adDescripcion=item.descripcion; //valor de la BD
                this.adId=item.idCaja;  //valor de la BD              
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

                axios.put('/api/Cajas/Activar/'+this.adId,{},configuracion).then(function(response){
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
                axios.put('/api/Cajas/Desactivar/'+this.adId,{},configuracion).then(function(response){
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




