<template>
    <v-layout align-start>
        <v-flex>

            <!-- Encabezado -->
            <v-toolbar flat color="white">
                <v-toolbar-title class="primary--text">Impuesto</v-toolbar-title>
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
                                    <v-text-field v-model="tipodocumento" label="Tipo de Documento"></v-text-field>
                                </v-flex>

                                <v-flex xs12 sm4 md4 lg4 xl4>
                                    <v-text-field 
                                    prefix="Q."
                                    type="number" 
                                    v-model="tasa" 
                                    label="Tasa %">
                                    
                                    </v-text-field>
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

                   

            </v-toolbar>
                

            <!-- DataTable -->
            <v-data-table
                :headers="headers"
                :items="impuestos"
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
                                
                            
                                

                            <!-- Columnas del DataTable (Nombre Descripcion) -->  
                            </td>
                                  
                                <td>{{ props.item.tipoDocumento }}</td>
                                <td>{{ props.item.descripcion }}</td> 
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
                impuestos:[],                            
                dialog: false,
               
               headers: [
               { text: 'Opciones', value: 'actions', sortable: false }, 
                                       
                    { text: 'Tipo Documento', value: 'tipodocumento' },  
                    { text: 'Descripción', value: 'descripcion', sortable: false  },                 
                    { text: 'Tasa', value: 'tasa', sortable: false  }
                                    
                ],
                
                search: '',
                editedIndex: -1,

                // variables de Impuesto
                id: '',
                tipodocumento: '',
                descripcion: '',
                tasa: '',

                

                valida: 0,       //validar los datos
                validaMensaje:[], //validar el msj

                adModal: 0,
                adAccion: 0,

                adId: ''             
            }


        },




        computed: {
            //evaluar si la variable es 1 o -1 para nuevo y actualizar
            formTitle () {
                return this.editedIndex === -1 ? 'Nuevo Impuesto' : 'Actualizar Impuesto'
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
            //Listas Impuesto
            listar(){
                let me=this;
                let header={"Authorization" : "Bearer " + this.$store.state.token};
                let configuracion= {headers : header};
                axios.get('/api/Impuestos/Listar', configuracion).then(function(response){
                    //console.log(response);
                    me.impuestos=response.data;
                }).catch(function(error){
                    console.log(error);
                });
            },

                   
            //Editar Impuestos
            editItem (item) {
                this.id=item.idImpuesto; //valores del DB
                this.tipodocumento=item.tipoDocumento;
                this.descripcion=item.descripcion;
                this.tasa=item.tasa;

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
                this.tipodocumento="";
                this.descripcion="";
                this.tasa="";
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
                    axios.put('/api/Impuestos/Actualizar',{
                        'idimpuesto':me.id,
                        'descripcion': me.descripcion,
                        'tipodocumento': me.tipodocumento,
                        'tasa': parseFloat( me.tasa),
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
                    axios.post('/api/Impuestos/Crear',{
                        'Descripcion': me.descripcion,
                        'tipodocumento': me.tipodocumento,
                        'tasa': parseFloat( me.tasa), 
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

            


            
        }        
    }






</script>




