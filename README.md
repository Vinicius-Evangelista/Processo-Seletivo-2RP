# Processo-Seletivo-2RP 🔵
Esse repositório foi desenvolvido com o intuito apresentar todo o código fonte utilizado para solucionar a demanda que foi entregue. 

## Como utilizar aplicação ? 
> Tenha o NodeJS e o framework dotnet em sua máquina assim como o Git, casa não os tenha:
 - https://nodejs.org/en/
 - https://dotnet.microsoft.com/en-us/
 - https://git-scm.com/
 
 > Entra na pasta "processo-seletivo-2RP-rest-api" e insira o comando:
 - dotnet run (Para iniciar a API)
 
 > Entra na pasta "processo-2rp-front-end" e insira dos comando:
 - npm i
 - npm start
 
> Pronto 😀! Basta que o banco estava ativo na AWS e tudo estará funcionando perfeitamente.

## Demanda 🤔: 
- Criar um sistema que possibilite o cadastro e login de usuários

## Funções ✅ :
- [x] Cadastrar um novo usuário;
- [x] Listar informações de um usuário;
- [x] Alterar o nome e o tipo de um usuário;
- [x] Excluir um usuário;
- [x] Alterar o status de um usuário (ativo ou inativo)
- [x] Tipos de usuário.

##
Regras de negócio 🥇
- [x] A tabela usuários deve conter os campos nome, senha, tipo, email e
status;
- [x] A tabela de tipos deve ter o tipo do usuário (geral, admin, root)
- [x] Um usuário pode ter apenas um único tipo;
- [x] Apenas usuários do tipo root e admin podem cadastrar novos usuários;
- [x] Apenas usuários do tipo root e admin podem alterar qualquer informa-
  ção do usuário (inclusive status);
- [x] Apenas usuários root podem excluir usuários;
- [x] Usuários do tipo geral só têm acesso a funcionalidade de listar infor-
mações de seu próprio usuário, bem como alterar suas próprias infor-
mações;
- [x] O login deve ser feito com email e senha;


## Tecnologias Utilizadas 👨‍💻:
<p float="left">
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/amazonwebservices/amazonwebservices-plain-wordmark.svg" width=110 height=110 />
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/microsoftsqlserver/microsoftsqlserver-plain-wordmark.svg" width=110 height=110/>
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dotnetcore/dotnetcore-original.svg" width=110 height=110 />
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/react/react-original.svg"  width=110 height=110 />    
</p>


- AWS: Utilizada como serviço em nuvem para hospedar o banco de dados. 
- SQL Server: Banco de dados utilizado para armazenar e gerenciar os dados.
- .NET: A API REST foi feita com C# utilizando o ambiente .Net como framework.
- React: Bibilioteca de JavaScript utilizada para densenvolver o front-end.


## Ferramentas secundárias  :
<p float="left">
 <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/trello/trello-plain.svg" width=110 height=90/>
 <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/figma/figma-original.svg" width=110 height=90/>
</p>

- Trello: Utilizado como auxiliador nas tarefas a serem entregue e organização, segue o link do board: 
- https://trello.com/b/RIgJhdFv/projeto-processo-seletivo-2rp 
- Figma: Utilizado na criacão dos layouts de baixa e de alta da aplicação como um todo.
