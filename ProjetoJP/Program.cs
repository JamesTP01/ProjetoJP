Conexao db = new Conexao();

db.Conectar();

AlunoRepositorio alunoRepositorio = new AlunoRepositorio(db.conn);

var teste = "";
int opcoes = 0;

while (opcoes != 5)
{
    opcoes = Menu();
    Console.Clear();
    switch (opcoes)
    {
        case 1:
            CadastrarAluno();
            break;
        case 2:

            break;
        case 3:

            break;
        case 4:

            break;
        case 5:
            Console.WriteLine("ENCERRANDO PROGRAMA....");
            break;
    }
}
Console.ReadKey();
static int Menu()
{
    Console.WriteLine("MENU DE OPÇÕES");
    Console.WriteLine("===================");
    Console.WriteLine("[1] Cadastrar Aluno");
    Console.WriteLine("[2] Consultar Aluno");
    Console.WriteLine("[3] Alterar dados do aluno");
    Console.WriteLine("[4] Excluir Aluno");
    Console.WriteLine("[5] Sair");

    int opcoes = int.Parse(Console.ReadLine());
    return opcoes;
}

void CadastrarAluno()
{
    Aluno aluno = new Aluno();

    Console.WriteLine("Preencha os dados solicitados do Aluno");

    Console.WriteLine("Nome Completo");
    aluno.Nome = Console.ReadLine();

    Console.WriteLine("Idade");
    aluno.Idade = int.Parse(Console.ReadLine());

    Console.WriteLine("Cpf");
    aluno.Cpf = Console.ReadLine();

    alunoRepositorio.InserirAluno(db, aluno);

}

using System;
using System.Data.SqlClient;
// ===== CLASSE CONEXÃO =====
public class Conexao
{
   public SqlConnection conn; new SqlConnection(Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="C:\Users\jemes.silva\source\repos\MEU BANCO MDF.mdf";Integrated Security=True;Connect Timeout=30;Encrypt=True
   public void Conectar()
   {
       string connectionString = "Server=SEU_SERVIDOR;Database=EscolaDB;Trusted_Connection=True;";
       conn = new SqlConnection(connectionString);
       conn.Open();
   }
   public void Desconectar()
   {
       if (conn != null && conn.State == System.Data.ConnectionState.Open)
           conn.Close();
   }
}
// ===== CLASSE ALUNO =====
public class Aluno
{
   public int Id { get; set; }
   public string Nome { get; set; }
   public int Idade { get; set; }
   public string DataNascimento { get; set; }
   public string Cpf { get; set; }
   public string Cep { get; set; }
   public string Endereco { get; set; }
   public string Numero { get; set; }
   public string Bairro { get; set; }
   public string Cidade { get; set; }
   public string Estado { get; set; }
   public double? Nota1 { get; set; }
   public double? Nota2 { get; set; }
   public double? Media { get; set; }
}
// ===== CLASSE USUÁRIO =====
public class Usuario
{
   public int Id { get; set; }
   public string NomeUsuario { get; set; }
   public string Senha { get; set; }
   public string Tipo { get; set; }
}
// ===== REPOSITORIO ALUNO =====
public class AlunoRepositorio
{
   private SqlConnection conn;
   public AlunoRepositorio(SqlConnection connection)
   {
       conn = connection;
   }
   public void InserirAluno(Aluno aluno)
   {
       string sql = @"INSERT INTO Alunos
           (Nome, Idade, DataNascimento, Cpf, Cep, Endereco, Numero, Bairro, Cidade, Estado)
           VALUES (@Nome, @Idade, @DataNascimento, @Cpf, @Cep, @Endereco, @Numero, @Bairro, @Cidade, @Estado)";
       using (SqlCommand cmd = new SqlCommand(sql, conn))
       {
           cmd.Parameters.AddWithValue("@Nome", aluno.Nome);
           cmd.Parameters.AddWithValue("@Idade", aluno.Idade);
           cmd.Parameters.AddWithValue("@DataNascimento", aluno.DataNascimento);
           cmd.Parameters.AddWithValue("@Cpf", aluno.Cpf);
           cmd.Parameters.AddWithValue("@Cep", aluno.Cep);
           cmd.Parameters.AddWithValue("@Endereco", aluno.Endereco);
           cmd.Parameters.AddWithValue("@Numero", aluno.Numero);
           cmd.Parameters.AddWithValue("@Bairro", aluno.Bairro);
           cmd.Parameters.AddWithValue("@Cidade", aluno.Cidade);
           cmd.Parameters.AddWithValue("@Estado", aluno.Estado);
           cmd.ExecuteNonQuery();
       }
   }
   public void ConsultarAluno(string cpf)
   {
       string sql = "SELECT * FROM Alunos WHERE Cpf = @Cpf";
       using (SqlCommand cmd = new SqlCommand(sql, conn))
       {
           cmd.Parameters.AddWithValue("@Cpf", cpf);
           using (SqlDataReader reader = cmd.ExecuteReader())
           {
               if (reader.Read())
               {
                   Console.WriteLine($"Nome: {reader["Nome"]}");
                   Console.WriteLine($"Idade: {reader["Idade"]}");
                   Console.WriteLine($"Cidade: {reader["Cidade"]}");
                   Console.WriteLine($"Nota1: {reader["Nota1"]}");
                   Console.WriteLine($"Nota2: {reader["Nota2"]}");
                   Console.WriteLine($"Média: {reader["Media"]}");
               }
               else
               {
                   Console.WriteLine("Aluno não encontrado!");
               }
           }
       }
   }
   public void AlterarAluno(Aluno aluno)
   {
       string sql = @"UPDATE Alunos SET Nome=@Nome, Idade=@Idade, Cidade=@Cidade WHERE Cpf=@Cpf";
       using (SqlCommand cmd = new SqlCommand(sql, conn))
       {
           cmd.Parameters.AddWithValue("@Nome", aluno.Nome);
           cmd.Parameters.AddWithValue("@Idade", aluno.Idade);
           cmd.Parameters.AddWithValue("@Cidade", aluno.Cidade);
           cmd.Parameters.AddWithValue("@Cpf", aluno.Cpf);
           cmd.ExecuteNonQuery();
       }
   }
   public void ExcluirAluno(string cpf)
   {
       string sql = "DELETE FROM Alunos WHERE Cpf=@Cpf";
       using (SqlCommand cmd = new SqlCommand(sql, conn))
       {
           cmd.Parameters.AddWithValue("@Cpf", cpf);
           cmd.ExecuteNonQuery();
       }
   }
   public void LancarNotas(string cpf, double nota1, double nota2)
   {
       double media = (nota1 + nota2) / 2;
       string sql = @"UPDATE Alunos SET Nota1=@Nota1, Nota2=@Nota2, Media=@Media WHERE Cpf=@Cpf";
       using (SqlCommand cmd = new SqlCommand(sql, conn))
       {
           cmd.Parameters.AddWithValue("@Nota1", nota1);
           cmd.Parameters.AddWithValue("@Nota2", nota2);
           cmd.Parameters.AddWithValue("@Media", media);
           cmd.Parameters.AddWithValue("@Cpf", cpf);
           cmd.ExecuteNonQuery();
       }
   }
}
// ===== REPOSITORIO USUARIO =====
public class UsuarioRepositorio
{
   private SqlConnection conn;
   public UsuarioRepositorio(SqlConnection connection)
   {
       conn = connection;
   }
   public Usuario Login(string usuario, string senha)
   {
       string sql = "SELECT * FROM Usuarios WHERE Usuario=@Usuario AND Senha=@Senha";
       using (SqlCommand cmd = new SqlCommand(sql, conn))
       {
           cmd.Parameters.AddWithValue("@Usuario", usuario);
           cmd.Parameters.AddWithValue("@Senha", senha);
           using (SqlDataReader reader = cmd.ExecuteReader())
           {
               if (reader.Read())
               {
                   return new Usuario
                   {
                       Id = (int)reader["Id"],
                       NomeUsuario = reader["Usuario"].ToString(),
                       Senha = reader["Senha"].ToString(),
                       Tipo = reader["Tipo"].ToString()
                   };
               }
           }
       }
       return null;
   }
}
// ===== PROGRAMA PRINCIPAL =====
class Program
{
   static Conexao db = new Conexao();
   static AlunoRepositorio alunoRepositorio;
   static UsuarioRepositorio usuarioRepositorio;
   static Usuario usuarioLogado;
   static void Main()
   {
       db.Conectar();
       alunoRepositorio = new AlunoRepositorio(db.conn);
       usuarioRepositorio = new UsuarioRepositorio(db.conn);
       usuarioLogado = FazerLogin();
       if (usuarioLogado == null)
       {
           Console.WriteLine("Usuário ou senha inválidos!");
       }
       else
       {
           if (usuarioLogado.Tipo == "ADMIN") MenuAdministrativo();
           else if (usuarioLogado.Tipo == "PROFESSOR") MenuProfessor();
           else if (usuarioLogado.Tipo == "ALUNO") MenuAluno();
       }
       db.Desconectar();
   }
   static Usuario FazerLogin()
   {
       Console.WriteLine("==== LOGIN ====");
       Console.Write("Usuário: ");
       string user = Console.ReadLine();
       Console.Write("Senha: ");
       string pass = Console.ReadLine();
       return usuarioRepositorio.Login(user, pass);
   }
   static void MenuAdministrativo()
   {
       int op = 0;
       while (op != 5)
       {
           Console.Clear();
           Console.WriteLine("=== MENU ADMINISTRATIVO ===");
           Console.WriteLine("[1] Cadastrar Aluno");
           Console.WriteLine("[2] Consultar Aluno");
           Console.WriteLine("[3] Alterar dados do Aluno");
           Console.WriteLine("[4] Excluir Aluno");
           Console.WriteLine("[5] Sair");
           op = int.Parse(Console.ReadLine());
           switch (op)
           {
               case 1: CadastrarAluno(); break;
               case 2: ConsultarAluno(); break;
               case 3: AlterarAluno(); break;
               case 4: ExcluirAluno(); break;
           }
       }
   }
   static void MenuProfessor()
   {
       int op = 0;
       while (op != 2)
       {
           Console.Clear();
           Console.WriteLine("=== MENU PROFESSOR ===");
           Console.WriteLine("[1] Lançar Notas");
           Console.WriteLine("[2] Sair");
           op = int.Parse(Console.ReadLine());
           if (op == 1) LancarNotas();
       }
   }
   static void MenuAluno()
   {
       Console.Clear();
       Console.Write("Digite seu CPF: ");
       string cpf = Console.ReadLine();
       alunoRepositorio.ConsultarAluno(cpf);
       Console.ReadKey();
   }
   static void CadastrarAluno()
   {
       Aluno aluno = new Aluno();
       Console.Write("Nome: "); aluno.Nome = Console.ReadLine();
       Console.Write("Idade: "); aluno.Idade = int.Parse(Console.ReadLine());
       Console.Write("Data Nascimento: "); aluno.DataNascimento = Console.ReadLine();
       Console.Write("CPF: "); aluno.Cpf = Console.ReadLine();
       Console.Write("CEP: "); aluno.Cep = Console.ReadLine();
       Console.Write("Endereço: "); aluno.Endereco = Console.ReadLine();
       Console.Write("Número: "); aluno.Numero = Console.ReadLine();
       Console.Write("Bairro: "); aluno.Bairro = Console.ReadLine();
       Console.Write("Cidade: "); aluno.Cidade = Console.ReadLine();
       Console.Write("Estado: "); aluno.Estado = Console.ReadLine();
       alunoRepositorio.InserirAluno(aluno);
       Console.WriteLine("Aluno cadastrado com sucesso!");
       Console.ReadKey();
   }
   static void ConsultarAluno()
   {
       Console.Write("Digite o CPF: ");
       alunoRepositorio.ConsultarAluno(Console.ReadLine());
       Console.ReadKey();
   }
   static void AlterarAluno()
   {
       Aluno aluno = new Aluno();
       Console.Write("CPF do aluno: "); aluno.Cpf = Console.ReadLine();
       Console.Write("Novo Nome: "); aluno.Nome = Console.ReadLine();
       Console.Write("Nova Idade: "); aluno.Idade = int.Parse(Console.ReadLine());
       Console.Write("Nova Cidade: "); aluno.Cidade = Console.ReadLine();
       alunoRepositorio.AlterarAluno(aluno);
       Console.WriteLine("Dados alterados!");
       Console.ReadKey();
   }
   static void ExcluirAluno()
   {
       Console.Write("CPF do aluno: ");
       alunoRepositorio.ExcluirAluno(Console.ReadLine());
       Console.WriteLine("Aluno excluído!");
       Console.ReadKey();
   }
   static void LancarNotas()
   {
       Console.Write("CPF do aluno: ");
       string cpf = Console.ReadLine();
       Console.Write("Nota 1: ");
       double nota1 = double.Parse(Console.ReadLine());
       Console.Write("Nota 2: ");
       double nota2 = double.Parse(Console.ReadLine());
       alunoRepositorio.LancarNotas(cpf, nota1, nota2);
       Console.WriteLine("Notas lançadas!");
       Console.ReadKey();
   }
}
