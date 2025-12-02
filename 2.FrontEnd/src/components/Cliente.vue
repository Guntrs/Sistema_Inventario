<template>
    <v-layout align-start>
        <v-flex>

            <!-- Encabezado -->
            <v-toolbar flat color="white">
                <v-toolbar-title class="primary--text">Clientes</v-toolbar-title>
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

                                            <v-flex xs12 sm6 md6>
                                                 <v-text-field v-model="nombre" label="Nombre">
                                            </v-text-field>
                                            </v-flex>       
                                            <v-flex xs12 sm6 md6>
                                                 <v-text-field v-model="codigopersona" label="Codigo">
                                            </v-text-field>
                                            </v-flex>    
                                            
                                            <v-flex xs12 sm6 md6>
                                                <v-select v-model="tipodocumento"
                                                :items="documentos" label="Tipo Documento">
                                                </v-select>
                                            </v-flex>
                                            
                                          
                                            <v-flex xs12 sm6 md6>
                                                <v-text-field v-model="numerodocumento" label="Número Documento">
                                                </v-text-field>
                                            </v-flex>
                                            <v-flex xs12 sm6 md6>
                                                <v-text-field v-model="direccion" label="Dirección">
                                                </v-text-field>
                                            </v-flex>
                                            <v-flex xs12 sm6 md6>
                                                <v-text-field v-model="telefono" label="Teléfono">
                                                </v-text-field>
                                            </v-flex>
                                            <v-flex xs12 sm6 md6>
                                                <v-text-field v-model="email" label="Email">
                                                </v-text-field>
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

                  

            </v-toolbar>
                

            <!-- DataTable -->
            <v-data-table
                :headers="headers"
                :items="clientes"
                :search="search"
                class="elevation-1"
            >
              
                <template v-slot:item="props">
                    <tr>
                        <td class="justify-center layout px-0">
                                
                                                     
                            <v-icon  color="black" small class="mr-2" @click="editItem(props.item)" >
                                edit
                            </v-icon>                          
                           
                                

                            <!-- Columnas del DataTable (Nombre Descripcion) -->  
                            </td>
                                <td >
                                    <strong>
                                        <div >
                                            <span class="black--text">
                                                {{ props.item.nombre }}
                                            </span>
                                        </div>
                                    </strong>                       
                                </td>    
                               
                                <td>{{ props.item.codigoPersona }}</td>
                                <td>{{ props.item.tipoPersona }}</td>
                                <td>{{ props.item.tipoDocumento }}</td>
                                <td >
                                    <strong>
                                        <div >
                                            <span class="black--text">
                                                {{ props.item.numeroDocumento }}
                                            </span>
                                        </div>
                                    </strong>                       
                                </td>  
                                
                                <td>{{ props.item.direccion }}</td>
                                <td>{{ props.item.telefono }}</td>
                                <td>{{ props.item.email }}</td>  
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

    
import axios from 'axios'
    export default {
        data(){
            return {
                clientes:[],                
                dialog: false,
               headers: [
                               
                    { text: 'Opciones', value: 'opciones', sortable: false },
                    { text: 'Nombre', value: 'nombre' },
                    { text: 'Codigo', value: 'codigopersona' },
                    { text: 'Tipo Persona', value: 'tipopersona' },
                    { text: 'Tipo Documento', value: 'tipodocumento' },
                    { text: 'Número Documento', value: 'numerodocumento', sortable: false  },
                    { text: 'Dirección', value: 'direccion', sortable: false  },
                    { text: 'Teléfono', value: 'telefono', sortable: false  },
                    { text: 'Email', value: 'email', sortable: false  }             
                ],
                
                search: '',
                editedIndex: -1,
                id: '',
                nombre:'',
                codigopersona:'',

                tipodocumento: '',
                documentos: ['NIT','OTRO','NO.'],
                numerodocumento: '',
                direccion: '',
                telefono: '',
                email: '',
                valida: 0,
                validaMensaje:[],
                adModal: 0,
                adAccion: 0,
                adNombre: '',
                adId: ''    
                       
            }
        },

        computed: {
            formTitle () {
                return this.editedIndex === -1 ? 'Nuevo Cliente' : 'Actualizar Cliente'
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

        methods:{
             listar(){
                let me=this;
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                
                axios.get('api/Personas/ListarClientes', configuracion).then(function(response){
                    //console.log(response);
                    me.clientes=response.data;
                }).catch(function(error){
                    console.log(error);
                });
            },
          
         //metodo
            editItem (item) {
                //como viene la variable desde el BackEnd
                this.id=item.idPersona;
                this.nombre=item.nombre;
                this.codigopersona=item.codigoPersona;
                this.tipodocumento=item.tipoDocumento;
                this.numerodocumento=item.numeroDocumento;
                this.direccion=item.direccion;
                this.telefono=item.telefono;
                this.email=item.email;
                this.editedIndex=1;
                this.dialog = true
            },      

            close () {
                this.dialog = false;
                this.limpiar();
            },

            //metodo Limpiar
            limpiar(){
                this.id="";
                this.nombre="";
                this.codigopersona="";
                this.tipodocumento="";
                this.numerodocumento="";
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

                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};

                if (this.editedIndex > -1) {
                    //Código para editar
                    //Código para guardar
                    let me=this;                                
                    axios.put('api/Personas/Actualizar',{
                        'idpersona':me.id,
                        'tipopersona':'Cliente',
                        'nombre':me.nombre,
                        'tipodocumento': me.tipodocumento,
                        'numerodocumento':me.numerodocumento,
                        'direccion':me.direccion,
                        'telefono': me.telefono,
                        'email':me.email,
                        'codigopersona':me.codigopersona      
                    }, configuracion).then(function(response){
                        me.close();
                        me.listar();
                        me.limpiar();                        
                    }).catch(function(error){
                        console.log(error);
                    });
                } else {
                    //Código para guardar
                    let me=this;
                    axios.post('api/Personas/Crear',{
                        'tipopersona':'Cliente',
                        'nombre':me.nombre,
                        'tipodocumento': me.tipodocumento,
                        'numerodocumento':me.numerodocumento,
                        'direccion':me.direccion,
                        'telefono': me.telefono,
                        'email':me.email,
                        'codigopersona':me.codigopersona 
                    },configuracion).then(function(response){
                        me.close();
                        me.listar();
                        me.limpiar();                        
                    }).catch(function(error){
                        console.log(error);
                    });
                }
            },

            //metodo Validar
            validar(){
                this.valida=0;
                this.validaMensaje=[];

                if (this.nombre.length<3 || this.nombre.length>100){
                    this.validaMensaje.push("El nombre debe tener más de 3 caracteres y menos de 100 caracteres.");
                }
                if (!this.email){
                    this.validaMensaje.push("Error del Correo");
                }
                
                if (this.validaMensaje.length){
                    this.valida=1;
                }
                return this.valida;
            },



           

        }        
    }






</script>




