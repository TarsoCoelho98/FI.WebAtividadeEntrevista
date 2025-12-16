## Observação sobre execução do projeto

Ao clonar este repositório, pode ocorrer um erro na primeira execução informando que o arquivo
csc.exe não foi encontrado no caminho "bin\roslyn".

### Motivo
O projeto utiliza o compilador Roslyn por meio do pacote NuGet  
"Microsoft.CodeDom.Providers.DotNetCompilerPlatform", responsável pela compilação das views Razor em tempo de execução.  
Após o clone, o diretório `bin\roslyn` ainda não existe, pois ele é gerado durante o processo de build.

### Como resolver

**Opção 1 (recomendada):**
1. Abrir a solution no Visual Studio
2. Definir o projeto FI.WebAtividadeEntrevista como projeto de inicialização
3. Executar o Clean + Rebuild no projeto FI.WebAtividadeEntrevista.
4. Executar o projeto normalmente

**Opção 2 (alternativa):**
1. Executar o comando "dotnet restore" na pasta onde se encontra o arquivo .sln.
2. Executar o Clean + Rebuild novamente no projeto FI.WebAtividadeEntrevista.
