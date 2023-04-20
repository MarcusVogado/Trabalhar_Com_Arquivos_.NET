using System;
using System.IO;
/*
              Listar todos os diretórios
Uma tarefa que você fará muitas vezes com a classe Directory é listar
(ou enumerar) diretórios. Por exemplo, a Tailwind Traders tem uma pasta 
raiz chamada stores. Nessa pasta, há subpastas organizadas por número de
loja e dentro dessas pastas estão o total de
vendas e os arquivos de inventário. A estrutura é semelhante a este exemplo:*/
IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();

    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        // The file name will contain the full path, so only check the end of it
        if (file.EndsWith("sales.json"))
        {
            salesFiles.Add(file);
        }
    }

    return salesFiles;
}

//Para ler e listar os nomes dos diretórios de nível superior,
//use a função Directory.EnumerateDirectories.
IEnumerable<string> listOfDirectories = Directory.EnumerateDirectories("Stores");

foreach (var dir in listOfDirectories)
{
    Console.WriteLine(dir);
}

//Listar arquivos em um diretório específico
//Para listar os nomes de todos os arquivos
//em um diretório, você pode usar a função Directory.EnumerateFiles.

IEnumerable<string> files = Directory.EnumerateFiles("stores");

foreach (var file in files)
{
    Console.WriteLine(file);
}

/*Listar todo o conteúdo de um diretório e todos os subdiretórios
Ambas as funções Directory.EnumerateDirectories e Directory.EnumerateFiles
têm uma sobrecarga que aceita um parâmetro para especificar um padrão de 
pesquisa a que os arquivos e diretórios devem corresponder.
Elas também têm outra sobrecarga que aceita um parâmetro para indicar se
é preciso percorrer recursivamente uma pasta especificada e todas as suas subpastas.*/

IEnumerable<string> allFilesInAllFolders = Directory.EnumerateFiles("stores", "*.txt", SearchOption.AllDirectories);

foreach (var file in allFilesInAllFolders)
{
    Console.WriteLine(file);
}
/*Trabalhar com diretórios especiais
O .NET funciona em todos os lugares: no Windows, no macOS, no Linux e, até mesmo,
em sistemas operacionais móveis, como o iOS e o Android. Cada sistema operacional
pode ou não ter o conceito de pastas especiais do sistema, como um diretório base
dedicado a arquivos específicos do usuário, um diretório da área de trabalho ou
um diretório para armazenar arquivos temporários.
Esses tipos de diretórios especiais variam conforme cada sistema operacional.
É complicado saber a estrutura do diretório de cada sistema operacional e executar 
alternâncias de acordo com o SO atual.
A enumeração System.Environment.SpecialFolder especifica constantes para recuperar
caminhos para pastas especiais do sistema.
O código a seguir retorna o caminho para o equivalente à pasta Meus Documentos do Windows
ou ao diretório BASE do usuário em todos os outros sistemas operacionais,
mesmo que o código esteja sendo executado no Linux:*/

string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


/*Caracteres especiais do caminho
Sistemas operacionais diferentes usam caracteres diferentes para separar níveis de diretório.

Por exemplo, o Windows usa a barra invertida (stores\201), e o macOS usa a barra "/" (stores/201).

Para ajudar a usar o caractere correto, a classe Path contém o campo DirectorySeparatorChar.

O .NET interpreta de modo automático esse campo no caractere separador aplicável ao sistema 
operacional quando é preciso criar um caminho manualmente.*/

Console.WriteLine($"stores{Path.DirectorySeparatorChar}201");
//Saída Windowns stores\201
//Saída MacOs stores/201

/*Caminhos de junção
A classe Path funciona com o conceito de caminhos de arquivos e pastas, que são apenas cadeias de 
caracteres. É possível usar a classe Path para criar de modo automático caminhos adequados para 
sistemas operacionais específicos.
Por exemplo, se você quisesse obter o caminho para a pasta repositórios/201,
poderia usar a função Path.Combine para fazer isso.*/

Console.WriteLine(Path.Combine("stores", "201"));

/*Lembre-se de usar a classe Path.Combine ou Path.DirectorySeparatorChar em vez de cadeias de caracteres
hard-coding porque o programa pode estar em execução em vários sistemas operacionais diferentes.
A classe Path sempre formatará os caminhos corretamente para qualquer sistema operacional em que 
esteja sendo executada.*/


/*Determinar as extensões de nome de arquivo
A classe Path também pode exibir a extensão de um nome de arquivo.
Caso tenha um arquivo e queira saber se ele é um arquivo JSON, é possível usar a função 
Path.GetExtension.
*/
Console.WriteLine(Path.GetExtension("sales.json"));
/*Tudo que você precisa saber sobre um arquivo ou caminho
A classe Path contém muitos métodos diferentes que fazem várias coisas. Você pode
obter a maioria das informações sobre um diretório ou arquivo usando as classes DirectoryInfo
ou FileInfo, respectivamente.*/

string fileName = $"stores{Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}" +
    $"sales{Path.DirectorySeparatorChar}sales.json";

FileInfo info = new FileInfo(fileName);

Console.WriteLine($"Full Name: {info.FullName}{Environment.NewLine}Directory:" +
    $" {info.Directory}{Environment.NewLine}Extension:" +
    $" {info.Extension}{Environment.NewLine}Create Date: {info.CreationTime}");

/*Usar o diretório atual e combinar caminhos
No código Program.cs atual, você está passando a localização estática da pasta repositórios.
Vamos alterar esse código para usar o valor Directory.GetCurrentDirectory em vez de passar um 
nome de pasta estática.
No editor, insira o código a seguir na parte de cima da primeira linha do arquivo Program.cs.
Este código usa o método Directory.GetCurrentDirectory para obter o caminho para o diretório 
atual e armazená-lo em uma nova variável currentDirectory:*/

var currentDirectory = Directory.GetCurrentDirectory();
/*Insira o código a seguir após aquele que você acabou de adicionar. Esse código usa o método Path.
 * Combine para criar o caminho completo para o diretório lojas e o armazena em uma nova variável 
 * storesDirectory:*/
var storesDirectory = Path.Combine(currentDirectory, "stores");
/*Substitua a variável stores na função FindFiles pela nova variável storesDirectory:*/
var salesFiles = FindFiles(storesDirectory);
foreach (var file in salesFiles)
{
    Console.WriteLine(file);
}

List<string> allFiles = new List<string>();

var folderName= Path.Combine(currentDirectory, "stores");

var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);
foreach (var file in foundFiles)
{
    var extension = Path.GetExtension(file);
    if (extension == ".json")
    {
        allFiles.Add(file);
    }
}

foreach(var file in allFiles)
{
    Console.WriteLine(file);
}

/*Criar e excluir arquivos e diretórios programaticamente é um requisito comum para aplicativos 
 * de linha de negócios.
Até agora, você aprendeu a trabalhar com arquivos e diretórios usando a classe Directory. 
Você também pode usar a classe Directory para criar, excluir, copiar, mover e manipular de 
alguma outra forma os diretórios de um sistema programaticamente. É possível usar uma classe
análoga, File, para fazer o mesmo com os arquivos.
Aqui você aprenderá a usar as classes Directory e File para criar diretórios e arquivos.*/

/*Criar diretórios
Use o método Directory.CreateDirectory para criar diretórios. O seguinte método cria uma pasta
    chamada newDir dentro da pasta 201:*/

var filePath=Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "stores", "201", "newDir"));

/*Às vezes, você precisará verificar se um diretório já existe. Por exemplo, talvez seja necessário 
 * verificar antes de criar um arquivo em um diretório específico para evitar uma exceção que pode
 * fazer com que o programa pare abruptamente.
Para ver se um diretório já existe, use o método Directory.Exists:*/

bool doesDirectoryExist = Directory.Exists(filePath.ToString());
Console.WriteLine(doesDirectoryExist);

/*Criar arquivos
Você pode criar arquivos usando o método File.WriteAllText. Esse método recebe o caminho do arquivo
e os dados que você deseja gravar no arquivo. Se o arquivo já existir, ele será substituído.
Por exemplo, esse código cria um arquivo chamado greeting.txt com o texto "Olá, Mundo!" dentro:*/


File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "greeting.txt"), "Hello World!");

