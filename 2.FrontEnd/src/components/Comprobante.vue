<template>
    <v-layout align-start>
        <v-flex>

            <!-- Encabezado -->
            <v-toolbar flat color="white">
                <v-toolbar-title class="primary--text">Tipo de Comprobante</v-toolbar-title>
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
                               
                                    <v-flex xs12 sm12 md12>
                                    <v-text-field v-model="descripcion" label="Descripción"></v-text-field>
                                    </v-flex>

                                <v-flex xs12 sm12 md12>
                                    <v-text-field v-model="serie" label="Serie"></v-text-field>
                                </v-flex>

                                <v-flex xs12 sm12 md12>
                                    <v-text-field v-model.number="correlativo" label="Correlativo"></v-text-field>
                                </v-flex>

                               

                                <v-flex xs6 sm6 md6>
                                    <v-select v-model="idimpuesto"
                                    :items="impuestos" label="Impuesto">
                                    </v-select>
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
                :items="comprobante"
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

                            
                            <td>{{ props.item.descripcion }}</td>  
                            <td>{{ props.item.serie }}</td>
                            <td >
                                    <strong>
                                        <div >
                                            <span class="black--text">
                                             No.  {{ props.item.correlativo }}
                                            </span>
                                        </div>
                                    </strong>                       
                                </td> 
                            
                            <td >
                                    <strong>
                                        <div >
                                            <span class="black--text">
                                                {{ props.item.tasa }}%
                                            </span>
                                        </div>
                                    </strong>                       
                                </td> 
                           
                            
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
                comprobante:[],                
                dialog: false,
               headers: [
                    { text: 'Opciones', value: 'actions', sortable: false },                    
                  
                    { text: 'Descripción', value: 'descripcion', sortable: false  },
                    { text: 'Serie', value: 'serie', sortable: false  },
                    { text: 'Correlativo', value: 'correlativo', sortable: false  },
                    { text: 'TipoImpuesto', value: 'tipoImpuesto', sortable: false  },

                    { text: 'Estado', value: 'condicion', sortable: false  }                
                ],
                
                search: '',
                editedIndex: -1,
                
                id: '',
                idimpuesto:'',

                codigo: '',
                descripcion: '',
                serie: '',
                correlativo: 0,
                tipoImpuesto: '',

                impuestos:[],
               
                impuesto:'',

                valida: 0,       //validar los datos
                validaMensaje:[], //validar el msj
                adModal: 0,
                adAccion: 0,
                adDescripcion: '',
                adId: ''          
            }


        },

        computed: {
            //evaluar si la variable es 1 o -1 para nuevo y actualizar
            formTitle () {
                return this.editedIndex === -1 ? 'Nuevo Comprobante' : 'Actualizar Comprobante'
            }
        },

        watch: {
            dialog (val) {
            val || this.close()
            }
        },

        created () {
            this.listar();
            this.select();
        },

        //---METODOS----------------------
        methods:{
            //Listar
            listar(){
                let me=this;
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                axios.get('/api/Comprobantes/Listar',configuracion).then(function(response){
                    //console.log(response);
                    me.comprobante=response.data;
                }).catch(function(error){
                    console.log(error);
                });
            },

            select(){
                let me=this;
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                var categoriasArray=[];
                axios.get('api/Impuestos/Select',configuracion).then(function(response){
                    categoriasArray=response.data;
                    categoriasArray.map(function(x){
                        me.impuestos.push({text: x.tasa,value:x.idImpuesto}); //valores del Select BD
                    });
                }).catch(function(error){
                    console.log(error);
               });
            },

                   
            //Editar 
            editItem (item) {
                this.id=  item.idComprobante; //valores del DB
                this.idimpuesto=item.idImpuesto;
                this.descripcion=item.descripcion;
                this.serie = item.serie;
                this.correlativo = item.correlativo;
                this.tipoImpuesto= item.tipoImpuesto;
                this.editedIndex=1;
                this.dialog = true
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
                this.idimpuesto="";
                this.descripcion="";
                this.serie = "";
                this.correlativo = "";
                this.tipoImpuesto= "";
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
                    axios.put('/api/Comprobantes/Actualizar',{
                        'idcomprobante':me.id,
                        'idimpuesto':me.idimpuesto,
                        'descripcion': me.descripcion,
                        'serie':me.serie,
                        'correlativo':me.correlativo
                        
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
                    axios.post('/api/Comprobantes/Crear',{
                        'descripcion': me.descripcion,
                        'serie':me.serie,
                        'correlativo':me.correlativo,
                         'idimpuesto':me.idimpuesto, 
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

                if (this.descripcion.length<0 || this.descripcion.length>100){
                    this.validaMensaje.push("Favor ingresar la Descripción del Comprobante");
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
                this.adId=item.idComprobante;  //valor de la BD     

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
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                axios.put('/api/Comprobantes/Activar/'+this.adId,{},configuracion).then(function(response){
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
                axios.put('/api/Comprobantes/Desactivar/'+this.adId,{},configuracion).then(function(response){
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




