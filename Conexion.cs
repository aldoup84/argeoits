using System.Data;
using System.IO;
using System.Text;
using Mono.Data.Sqlite;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using System;

public class Conexion : MonoBehaviour
{
    InputField input1, AddUser, AddName, addPass,  AddEscuela, AddGrupo, AddStatus;    
    private string connection;
    private IDbConnection dbCon;
    private IDbCommand dbCmd;
    private IDataReader reader;
    private string pass = string.Empty;
	private int derecho;
    //private int status = 0;

    private void Start()
    {
		derecho = 1;		
		}

    public void AbrirConexion() {
        string p = "DataBase.db";
        string filePath;
        if (Application.isMobilePlatform)        {
            filePath = Application.persistentDataPath + "/" + p;
        }
        else{
            filePath = Application.streamingAssetsPath + "/" + p;
        }

        if (!File.Exists(filePath)) {
            CrearBaseDatos(filePath);
        }

        connection = "URI=file:" + filePath;
        Debug.Log("Conectando a la base de datos  " + connection);        
        dbCon = new SqliteConnection(connection);
        dbCon.Open();
    }

	void CrearBaseDatos(string filePath) {
		//	Debug.Log("Creando la base de datos");
		WWW loadDb = new WWW("jar:file//" + Application.dataPath + "!/assets/" + "Database.db");
		while (!loadDb.isDone) { }
		File.WriteAllBytes(filePath, loadDb.bytes);
		connection = "URI=file:" + filePath;
		dbCon = new SqliteConnection(connection);
		dbCon.Open();
		dbCmd = dbCon.CreateCommand();
		List<string> cmdText = new List<string>(){
			"CREATE TABLE IF NOT EXISTS 'VolFiguras' ( 'idFigura' INTEGER NOT NULL UNIQUE, 'Nombre'	TEXT NOT NULL, 'Marcador'	TEXT, 'Medida1'   REAL NOT NULL, 'Medida2'    REAL, 'Medida3' REAL NOT NULL, 'Area'   REAL NOT NULL, 'Volumen'    REAL NOT NULL, 'Nivel' INTEGER, PRIMARY KEY('idFigura'))",			
			"CREATE TABLE IF NOT EXISTS 'Usuarios' ('idUsuario'	TEXT NOT NULL UNIQUE, 'nombre'	TEXT NOT NULL, 'pass'	TEXT NOT NULL, 'status'	INTEGER NOT NULL, 'escuela'	TEXT, 'grupo'	TEXT, PRIMARY KEY('idUsuario'))",
			"CREATE TABLE IF NOT EXISTS 'Sesion' ('idSesion'	INTEGER PRIMARY KEY AUTOINCREMENT, 'idUsuario'	TEXT NOT NULL, 'Nombre'	TEXT NOT NULL, 'LastSesion'	TEXT)",
			"CREATE TABLE IF NOT EXISTS 'FigMarcs' ('idFigura'	INTEGER NOT NULL, 'Descrpcion'	TEXT NOT NULL, 'Marcador'	TEXT NOT NULL);",
			"CREATE TABLE IF NOT EXISTS 'FigInc' ('idFigura'	INTEGER NOT NULL UNIQUE,'idImagen'	TEXT NOT NULL,'descripcion'	TEXT NOT NULL,'marcador'	TEXT NOT NULL,'nivel'	INTEGER NOT NULL)",			
			"CREATE TABLE IF NOT EXISTS 'Ejercicios' ('Folio'	INTEGER NOT NULL, 'idSesion'	INTEGER, 'idEjercicio'	TEXT NOT NULL, 'idUsuario'	TEXT NOT NULL, 'aciertos'	INTEGER, 'errores'	INTEGER, 'tiempo'	INTEGER, 'puntos'	INTEGER, 'nivel'	REAL, 'dificultad'	TEXT, 'Fecha'	TEXT);",
			"CREATE TABLE IF NOT EXISTS 'ConstruirFigs' ('idFigura' INTEGER NOT NULL, 'figura1' TEXT NOT NULL, 'figura2'	TEXT NOT NULL,'nivel' INTEGER NOT NULL, 'Tipo 'TEXT)",
			"INSERT INTO 'VolFiguras' VALUES (1,'Prisma Triangular','Prisma_Triangular',3.0,5.0,10.0,7.5,75.0,1);",
			"INSERT INTO 'VolFiguras' VALUES (2,'Prisma Triangular','Prisma_Triangular',4.0,8.0,10.0,16.0,160.0,2);",
			"INSERT INTO 'VolFiguras' VALUES (3,'Prisma Triangular','Prisma_Triangular',6.0,9.0,20.0,27.0,540.0,3);",
			"INSERT INTO 'VolFiguras' VALUES (4,'Prisma Triangular','Prisma_Triangular',4.5,7.0,13.0,15.75,204.75,4);",
			"INSERT INTO 'VolFiguras' VALUES (5,'Prisma Triangular','Prisma_Triangular',3.8,9.0,16.0,17.1,273.6,5);",
			"INSERT INTO 'VolFiguras' VALUES (6,'Prisma Cuadrangular','Prisma_Cuadrangular',4.0,4.0,10.0,16.0,160.0,1);",
			"INSERT INTO 'VolFiguras' VALUES (7,'Prisma Cuadrangular','Prisma_Cuadrangular',5.0,5.0,15.0,25.0,375.0,2);",
			"INSERT INTO 'VolFiguras' VALUES (8,'Prisma Cuadrangular','Prisma_Cuadrangular',8.0,8.0,14.0,64.0,896.0,3);",
			"INSERT INTO 'VolFiguras' VALUES (9,'Prisma Cuadrangular','Prisma_Cuadrangular',2.0,2.0,7.5,4.0,30.0,4);",
			"INSERT INTO 'VolFiguras' VALUES (10,'Prisma Cuadrangular','Prisma_Cuadrangular',3.8,3.8,11.5,14.44,166.06,5);",
			"INSERT INTO 'VolFiguras' VALUES (11,'Prisma Pentagonal','Prisma_Pentagonal',2.4,1.65,5.0,9.9,49.5,1);",
			"INSERT INTO 'VolFiguras' VALUES (12,'Prisma Pentagonal','Prisma_Pentagonal',2.6,1.65,10.0,10.73,107.25,2);",
			"INSERT INTO 'VolFiguras' VALUES (13,'Prisma Pentagonal','Prisma_Pentagonal',2.5,2.3,15.0,14.38,281.25,3);",
			"INSERT INTO 'VolFiguras' VALUES (14,'Prisma Pentagonal','Prisma_Pentagonal',3.2,1.77,8.5,14.16,120.36,4);",
			"INSERT INTO 'VolFiguras' VALUES (15,'Prisma Pentagonal','Prisma_Pentagonal',2.9,3.22,12.2,23.35,284.81,5);",
			"INSERT INTO 'VolFiguras' VALUES (16,'Prisma Hexagonal','Prisma_Hexagonal',2.4,1.6,10.0,11.52,115.2,1);",
			"INSERT INTO 'VolFiguras' VALUES (17,'Prisma Hexagonal','Prisma_Hexagonal',3.2,2.0,7.5,19.2,144.0,2);",
			"INSERT INTO 'VolFiguras' VALUES (18,'Prisma Hexagonal','Prisma_Hexagonal',4.0,3.2,10.0,38.4,384.0,3);",
			"INSERT INTO 'VolFiguras' VALUES (19,'Prisma Hexagonal','Prisma_Hexagonal',5.0,2.74,8.7,41.1,357.57,4);",
			"INSERT INTO 'VolFiguras' VALUES (20,'Prisma Hexagonal','Prisma_Hexagonal',2.0,1.73,11.21,10.38,116.36,5);",
			"INSERT INTO 'VolFiguras' VALUES (21,'Prisma Decagonal','Prisma_Decagonal',2.0,2.0,14.0,20.0,280.0,1);",
			"INSERT INTO 'VolFiguras' VALUES (22,'Prisma Decagonal','Prisma_Decagonal',2.5,1.74,12.6,21.75,274.05,2);",
			"INSERT INTO 'VolFiguras' VALUES (23,'Prisma Decagonal','Prisma_Decagonal',3.0,2.22,10.28,33.3,342.32,3);",
			"INSERT INTO 'VolFiguras' VALUES (24,'Prisma Decagonal','Prisma_Decagonal',1.37,3.57,11.8,24.45,288.56,4);",
			"INSERT INTO 'VolFiguras' VALUES (25,'Prisma Decagonal','Prisma_Decagonal',2.14,4.77,13.28,51.04,677.8,5);",
			"INSERT INTO 'VolFiguras' VALUES (26,'Cilindro','Cilindro',2.0,3.1416,5.0,12.57,62.83,1);",
			"INSERT INTO 'VolFiguras' VALUES (27,'Cilindro','Cilindro',3.0,3.1416,10.0,28.27,282.74,2);",
			"INSERT INTO 'VolFiguras' VALUES (28,'Cilindro','Cilindro',5.0,3.1416,7.0,78.54,549.78,3);",
			"INSERT INTO 'VolFiguras' VALUES (29,'Cilindro','Cilindro',3.21,3.1416,13.8,32.37,446.72,4);",
			"INSERT INTO 'VolFiguras' VALUES (30,'Cilindro','Cilindro',2.93,3.1416,11.11,26.97,299.64,5);",
			"INSERT INTO 'Usuarios' VALUES ('0','Admin','Alem',0,'ITC','Admin')",
			"INSERT INTO 'Usuarios' VALUES ('1','Prueba','123',0,'SEPyC','1')",
			"INSERT INTO 'FigMarcs' VALUES (1,'Prisma Triangular','Prisma_Triangular');",
			"INSERT INTO 'FigMarcs' VALUES (6,'Prisma Cuadrangular','Prisma_Cuadrangular');",
			"INSERT INTO 'FigMarcs' VALUES (2,'Prisma Triangular','Prisma_Triangular');",
			"INSERT INTO 'FigMarcs' VALUES (3,'Prisma Triangular','Prisma_Triangular');",
			"INSERT INTO 'FigMarcs' VALUES (4,'Prisma Triangular','Prisma_Triangular');",
			"INSERT INTO 'FigMarcs' VALUES (5,'Prisma Triangular','Prisma_Triangular');",
			"INSERT INTO 'FigMarcs' VALUES (7,'Prisma Cuadrangular','Prisma_Cuadrangular');",
			"INSERT INTO 'FigMarcs' VALUES (8,'Prisma Cuadrangular','Prisma_Cuadrangular');",
			"INSERT INTO 'FigMarcs' VALUES (9,'Prisma Cuadrangular','Prisma_Cuadrangular');",
			"INSERT INTO 'FigMarcs' VALUES (10,'Prisma Cuadrangular','Prisma_Cuadrangular');",
			"INSERT INTO 'FigMarcs' VALUES (11,'Prisma Pentagonal','Prisma_Pentagonal');",
			"INSERT INTO 'FigMarcs' VALUES (12,'Prisma Pentagonal','Prisma_Pentagonal');",
			"INSERT INTO 'FigMarcs' VALUES (13,'Prisma Pentagonal','Prisma_Pentagonal');",
			"INSERT INTO 'FigMarcs' VALUES (14,'Prisma Pentagonal','Prisma_Pentagonal');",
			"INSERT INTO 'FigMarcs' VALUES (15,'Prisma Pentagonal','Prisma_Pentagonal');",
			"INSERT INTO 'FigMarcs' VALUES (16,'Prisma Hexagonal','Prisma_Hexagonal');",
			"INSERT INTO 'FigMarcs' VALUES (17,'Prisma Hexagonal','Prisma_Hexagonal');",
			"INSERT INTO 'FigMarcs' VALUES (18,'Prisma Hexagonal','Prisma_Hexagonal');",
			"INSERT INTO 'FigMarcs' VALUES (19,'Prisma Hexagonal','Prisma_Hexagonal');",
			"INSERT INTO 'FigMarcs' VALUES (20,'Prisma Hexagonal','Prisma_Hexagonal');",
			"INSERT INTO 'FigMarcs' VALUES (21,'Prisma Decagonal','Prisma_Decagonal');",
			"INSERT INTO 'FigMarcs' VALUES (22,'Prisma Decagonal','Prisma_Decagonal');",
			"INSERT INTO 'FigMarcs' VALUES (20,'Prisma Hexagonal','Prisma_Hexagonal');",
			"INSERT INTO 'FigMarcs' VALUES (23,'Prisma Decagonal','Prisma_Decagonal');",
			"INSERT INTO 'FigMarcs' VALUES (24,'Prisma Decagonal','Prisma_Decagonal');",
			"INSERT INTO 'FigMarcs' VALUES (25,'Prisma Decagonal','Prisma_Decagonal');",
			"INSERT INTO 'FigMarcs' VALUES (26,'Cilindro','Cilindro');",
			"INSERT INTO 'FigMarcs' VALUES (27,'Cilindro','Cilindro');",
			"INSERT INTO 'FigMarcs' VALUES (28,'Cilindro','Cilindro');",
			"INSERT INTO 'FigMarcs' VALUES (29,'Cilindro','Cilindro');",
			"INSERT INTO 'FigMarcs' VALUES (30,'Cilindro','Cilindro');",

			"INSERT INTO 'FigInc' VALUES (1,'figura1','Cilindro','FIG1-01',2);",
			"INSERT INTO 'FigInc' VALUES (2,'figura2','Cilindro Oblicuo','FIG1-02',3);",
			"INSERT INTO 'FigInc' VALUES (3,'figura3','Cono','FIG1-03',2);",
			"INSERT INTO 'FigInc' VALUES (4,'figura4','Cubo','FIG1-04',1);",
			"INSERT INTO 'FigInc' VALUES (5,'figura5','Dodecaedro','FIG1-05',3);",
			"INSERT INTO 'FigInc' VALUES (6,'figura6','Piramide Pentagonal','FIG1-06',2);",
			"INSERT INTO 'FigInc' VALUES (7,'figura7','Octaedro','FIG1-07',2);",
			"INSERT INTO 'FigInc' VALUES (8,'figura8','Piramide Trunca','FIG1-08',3);",
			"INSERT INTO 'FigInc' VALUES (9,'figura9','Prisma Pentagonal','FIG1-09',1);",
			"INSERT INTO 'FigInc' VALUES (10,'figura10','Tetraedro','FIG1-10',2);",
			"INSERT INTO 'FigInc' VALUES (11,'figura11','Cono Trunco','FIG1-11',3);",
			"INSERT INTO 'FigInc' VALUES (12,'figura12','Prisma Triangular','FIG1-12',1);",
			"INSERT INTO 'FigInc' VALUES (13,'figura13','Prisma Rectangular','FIG1-13',1);",

			"INSERT INTO 'Ejercicios' VALUES (1,1,'1','0',6,1,159.59,11,1.0,'Facil','9/26/2018');",
			"INSERT INTO 'Ejercicios' VALUES (2,2,'2','2',2,0,0,600,1.0,'Facil','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (3,2,'3','2',1,0,4.02,200,1.0,'Facil','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (4,2,'3','2',3,0,29.28,600,2.0,'Regular','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (5,2,'3','2',4,4,58.51,400,3.0,'Avanzado','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (6,2,'3','2',10,5,128.13,1500,2.0,'Regular','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (7,2,'3','2',1,0,21.6,200,1.0,'Facil','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (8,2,'3','2',3,1,58.82,500,2.0,'Regular','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (9,2,'3','2',4,2,75.09,600,3.0,'Avanzado','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (10,2,'3','2',8,2,132.96,1400,2.0,'Regular','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (11,2,'3','2',9,2,151.86,1600,3.0,'Avanzado','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (12,2,'3','2',10,2,166.23,1800,2.0,'Regular','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (13,4,'3','4',1,0,27.14,200,1.0,'Facil','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (14,4,'3','4',2,1,56.79,300,2.0,'Regular','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (15,4,'3','4',3,2,80.97,400,3.0,'Avanzado','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (16,4,'3','4',6,2,163.31,1000,2.0,'Regular','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (17,4,'3','4',7,2,187.92,1200,3.0,'Avanzado','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (18,4,'3','4',9,3,226.21,1500,2.0,'Regular','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (19,4,'3','4',10,3,243.83,1700,3.0,'Avanzado','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (20,4,'3','4',10,3,243.83,1700,2.0,'Regular','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (21,8,'3','8',1,0,4.53,200,1.0,'Facil','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (22,8,'3','8',3,1,31.28,500,2.0,'Regular','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (23,8,'3','8',4,1,49.35,700,3.0,'Avanzado','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (24,9,'3','9',1,0,4.81,200,1.0,'Facil','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (25,9,'3','9',3,1,32.16,500,2.0,'Regular','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (26,9,'3','9',4,2,47.8,600,3.0,'Avanzado','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (27,9,'3','9',7,3,101.16,1200,2.0,'Regular','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (28,11,'3','11',1,0,28.22,200,1.0,'Facil','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (29,11,'3','11',3,0,44.02,600,2.0,'Regular','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (30,11,'3','11',4,0,56.06,800,3.0,'Avanzado','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (31,11,'3','11',9,0,125.19,1800,2.0,'Regular','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (32,11,'3','11',10,0,150.26,2000,3.0,'Avanzado','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (33,11,'3','11',10,0,150.26,2000,2.0,'Regular','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (34,12,'3','12',1,0,20.03,200,1.0,'Facil','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (35,12,'3','12',3,0,53.86,600,2.0,'Regular','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (36,12,'3','12',4,0,63.29,800,3.0,'Avanzado','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (37,12,'3','12',9,1,133.94,1700,2.0,'Regular','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (38,12,'3','12',10,1,157.36,1900,3.0,'Avanzado','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (39,12,'3','12',10,1,157.36,1900,2.0,'Regular','3/26/2019');",
			"INSERT INTO 'Ejercicios' VALUES (40,14,'1','14',0,5,637.34,-500,1.0,'Muy Facil','3/27/2019');",
			"INSERT INTO 'Ejercicios' VALUES (41,14,'3','14',1,1,35.47,100,1.0,'Facil','3/27/2019');",
			"INSERT INTO 'Ejercicios' VALUES (42,14,'3','14',2,14,327.63,-900,2.0,'Regular','3/27/2019');",
			"INSERT INTO 'Ejercicios' VALUES (43,14,'3','14',3,15,372.09,-800,1.0,'Facil','3/27/2019');",
			"INSERT INTO 'Ejercicios' VALUES (44,14,'3','14',5,18,467.26,-600,2.0,'Regular','3/27/2019');",
			"INSERT INTO 'Ejercicios' VALUES (45,14,'3','14',1,1,29.7,100,1.0,'Facil','3/27/2019');",
			"INSERT INTO 'Ejercicios' VALUES (46,14,'3','14',2,4,132.27,100,2.0,'Regular','3/27/2019');",
			"INSERT INTO 'Ejercicios' VALUES (47,14,'3','14',3,4,148,300,1.0,'Facil','3/27/2019');",
			"INSERT INTO 'Ejercicios' VALUES (48,14,'3','14',1,1,31.49,100,1.0,'Facil','3/27/2019');",
			"INSERT INTO 'ConstruirFigs' VALUES (1,'Paralelo a la Base','Paralelo a la Base',1,'Cilindro');",
			"INSERT INTO 'ConstruirFigs' VALUES (2,'Paralelo a la Base','Paralelo a la Base',1,'Cilindro');",
			"INSERT INTO 'ConstruirFigs' VALUES (3,'Perpendicular','Perpendicular',1,'Cilindro');"
		};
		foreach ( var item in cmdText) {
			dbCmd.CommandText = item;
			dbCmd.ExecuteNonQuery();
		}
	
        dbCmd.CommandText = "INSERT INTO 'SESION' ('IdUsuario', 'Nombre', 'LASTSESION') VALUES ('0','Administrador','" +            
            System.DateTime.Now.Date.ToShortDateString() + "')";
        dbCmd.ExecuteNonQuery();
		CerrarConexion2();       
    }

    public void CerrarDataSet2()  {
        reader.Close();
        reader = null;
		CerrarConexion2();
      //  Debug.Log("Cerrando Conexion");
    }

	public void CerrarConexion2()
	{
		dbCmd.Dispose();
		dbCmd = null;
		dbCon.Close();
		dbCon = null;
	}

	

	public void Consultar() {
        input1 = GameObject.Find("txtUsuario").GetComponent<InputField>();
        if (input1.text.Equals(""))
        {
            Debug.Log("NO SE PUEDEN CONSULTAR CLAVES VACIOS");
            return;
        }
        AbrirConexion();
        dbCmd = dbCon.CreateCommand();
        dbCmd.CommandText = "Select * from Usuarios where idUsuario = '" + input1.text + "'";    // Es la consulta en codigo SQl
        reader = dbCmd.ExecuteReader();     
        while (reader.Read())
        { //Recupera los datos del DataSet
            string id = reader.GetString(0);
            string nombre = reader.GetString(1);
            pass = reader.GetString(2);
            derecho = reader.GetInt32(3);          
            GameObject.Find("lblNombre").GetComponent<Text>().text = nombre;
            GameObject.Find("txtPass").GetComponent<InputField>().ActivateInputField();
        }
		CerrarDataSet2();      
    }

    public void Select(){
        Consultar();
    }


    public bool RevisarInputs(){
        bool resultado = false;

        if (GameObject.Find("AddUsuario").GetComponent<InputField>().text.Equals("") || 
            GameObject.Find("InputNombre").GetComponent<InputField>().text.Equals("") ||
            GameObject.Find("InputPass").GetComponent<InputField>().text.Equals("") ||
            GameObject.Find("InputEscuela").GetComponent<InputField>().text.Equals("") ||
            GameObject.Find("InputGrupo").GetComponent<InputField>().text.Equals("")){
            resultado = true;
        }
        Debug.Log("Resultado" + resultado);
        return resultado;
    }

    public void Insert() {
    //    Debug.Log("Validando espaciados");
        if (RevisarInputs() == true || ExisteUsuario()){
            return;
        }
        AbrirConexion();
        dbCmd = dbCon.CreateCommand(); //   crea el comando para insertar los datos        
        dbCmd.CommandText = "INSERT INTO usuarios (idUsuario, nombre, pass, status, escuela, grupo) VALUES ('" +
            GameObject.Find("AddUsuario").GetComponent<InputField>().text + "', '" +
            GameObject.Find("InputNombre").GetComponent<InputField>().text + "', '" +
            GameObject.Find("InputPass").GetComponent<InputField>().text + "', 1, '" +
            GameObject.Find("InputEscuela").GetComponent<InputField>().text + "', '" +
            GameObject.Find("InputGrupo").GetComponent<InputField>().text + "')";
        dbCmd.ExecuteNonQuery();
		CerrarConexion2(); // Cerrando DataSet
		StartCoroutine(Mostrar("Registro guardado existosamente"));
		GameObject.Find("PanelRegistro").GetComponent<RectTransform>().localScale = Vector3.zero;
		GameObject.Find("LoginPanel").GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
	}

	public IEnumerator Mostrar(string mensaje) {
		GameObject.Find("pnlMensaje").GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
		GameObject.Find("txtMensaje").GetComponent<TextMeshProUGUI>().text = mensaje;
		yield return new WaitForSeconds(3);
		GameObject.Find("pnlMensaje").GetComponent<RectTransform>().localScale = Vector3.zero;		
	}

    public void Actualizar(){
        if (RevisarInputs() == false){
            return;
        }
        AbrirConexion();
        dbCmd = dbCon.CreateCommand(); //   La consulta de la tabla en la base de datos
     
        dbCmd.CommandText = "UPDATE usuarios SET nombre = '" + input1.GetComponent<Text>().text + "', NOMBRE = '" + input1.GetComponent<Text>().text + "' WHERE IdUsuario =  " + input1.GetComponent<Text>().text;
        dbCmd.ExecuteNonQuery();
        Debug.Log("Se ha Actualizado el registro");
		CerrarConexion2();
     }

    public void Recargar(){
        SceneManager.LoadScene(0);
    }

    public void LogIn(){
        InputField txtUsuario = GameObject.Find("txtUsuario").GetComponent<InputField>();
        InputField txtPass = GameObject.Find("txtPass").GetComponent<InputField>();
        if (txtUsuario.text.Equals("") || txtPass.text.Equals("")) {
		// Debug.Log("Favor de escribir usuario y contraseña");
            return;
        }

		AbrirConexion();
		dbCmd = dbCon.CreateCommand(); //   La consulta de la tabla en la base de datos

		dbCmd.CommandText = "Select count(*) from Usuarios where idUsuario = " + txtUsuario.text +  // Es la consulta en codigo SQl
		" and pass = '" + txtPass.text + "'";
		
		int scalar = Convert.ToInt32(dbCmd.ExecuteScalar());
		if (scalar == 1){			
			dbCmd.CommandText = "Select * from Usuarios where idUsuario = '" + txtUsuario.text
				+ "' and pass = '" + txtPass.text + "'";
			reader = dbCmd.ExecuteReader();     //Ejecuta la consulta

			while (reader.Read())
			{ //Recupera los datos del DataSet          
				string nombre = reader.GetString(1);
				derecho = reader.GetInt32(3);
				GameObject.Find("lblNombre").GetComponent<Text>().text = nombre;
				// salvando datos en la memoria interna del dispositivo
				PlayerPrefs.SetString("id", txtUsuario.text);
				PlayerPrefs.SetString("nombre", nombre);
				PlayerPrefs.SetInt("derechos", derecho);
			}

			reader.Close();
			reader = null;

			dbCmd.CommandText = "INSERT INTO 'Sesion' ('IdUsuario', 'Nombre', 'LastSesion') VALUES ('" +
					  GameObject.Find("txtUsuario").GetComponent<InputField>().text + "', '" +
					  GameObject.Find("lblNombre").GetComponent<Text>().text + "' , '" +
					  System.DateTime.Now.Date.ToShortDateString().ToString() + "')";
			dbCmd.ExecuteNonQuery();

			dbCmd.CommandText = "Select COUNT(*) from Sesion";
			reader = dbCmd.ExecuteReader();
			while (reader.Read()){
				PlayerPrefs.SetInt("idSesion", reader.GetInt32(0));
			}
			CerrarDataSet2();
			GameObject.Find("LoginPanel").GetComponent<RectTransform>().localScale = Vector3.zero;
			SceneManager.LoadScene("menu");			
		}
		else {
			StartCoroutine(Mostrar("Usuario o contraseña incorrecta"));
		}		
	}

	public void Ver(){
		ExisteUsuario();
	}

	public bool ExisteUsuario() {
		bool res = false;
		input1 = GameObject.Find("AddUsuario").GetComponent<InputField>();
		if (input1.text.Equals("")){
			Debug.Log("NO SE PUEDEN CONSULTAR CLAVES VACIOS");
			return true;
		}
		AbrirConexion();
		dbCmd = dbCon.CreateCommand();
		dbCmd.CommandText = "Select count(*) from Usuarios where idUsuario = " + input1.text + " and status = 1";    // Es la consulta en codigo SQl
		
		int scalar = Convert.ToInt32(dbCmd.ExecuteScalar());
		if (scalar == 0) { 
			res = false;
		}
		else {
			StartCoroutine(Mostrar("Usuario ya registrado"));
			res = true;
		}

		CerrarConexion2();
		return res;
	}
	public void OcultarLogIn() {
		GameObject.Find("LoginPanel").GetComponent<RectTransform>().localScale = Vector3.zero;
	}

	public void MostrarLogIn(){
		GameObject.Find("LoginPanel").GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
	}
}