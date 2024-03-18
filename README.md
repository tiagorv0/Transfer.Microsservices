<h1 align="center">FIAP - Transfer.Microsservices - TECH CHALLENGE 3</h1>
<br>

<h3 align="center">Integrantes do Grupo:</h3>
<br>
<li>Bruno Moura     - RM 350846</li>
<li>Marcos André    - RM 351923</li>
<li>Tiago Vazzoller - RM 351733</li>
<li>Victor Hugo     - RM 351315</li>
<br>

<h3 align="center">Projeto Transfer.Microsservices</h3>

<h3 align="center">Para poder rodar o projeto</h3>
<br>
<p>Clone o projeto usando Visual Studio 2022 ou Visual Studio Code</p>
<br>
<p>Tenha Instalado o .Net 8.0, caso não tenha, pode fazer download do link abaixo:</p>
 <pre> https://dotnet.microsoft.com/pt-br/download/dotnet/8.0</pre>
<br>
<p>Este projeto está usando o MongoDb, você pode usar a instancia do Mongo se estiver instalado em sua máquina ou em Docker:</p>
<p>MongoDb Compass:</p>
  <pre>https://www.mongodb.com/try/download/compass</pre>
<p>Via Docker:</p>
 <pre> docker run --name some-mongo -d mongo</pre>
<br>

<p>Este projeto está usando o RabbitMq, você pode usar a instancia do Rabbit via Docker:</p>
<p>Via Docker:</p>
 <pre> docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.13-management</pre>
<br>

<p>Após clonar você deve Configurar Projetos de Inicialização como a imagem a seguir:</p>
<p><img alt="Texto da propriedade alt" title="Propriedade title" src="https://github.com/tiagorv0/Transfer.Microsservices/blob/main/images/initialization.png" /></p>

<p>Definindo como Inicar e ISS Express: </p>
<li>Transfer.Account</li>
<li>Transfer.Api</li>
<li>Transfer.Notification</li>


<p>Ao rodar o projeto deve-se criar duas contas para realizar as transferências:</p>
<pre>api/account/create-account</pre>
<p>Crie os usuários com nome, transferKey e saldo da conta</p>
<p>TransferKey pode ser CPF, telefone ou email</p>

<p>Depois vá na rota:</p>
<pre>/api/transfer/create-transfer</pre>
<p>Insira a TransferKey de quem vai enviar(Sender) o valor e a TransferKey de quem vai receber(Receiver)</p>
<p>ScheduleDate pode deixar nulo</p>
