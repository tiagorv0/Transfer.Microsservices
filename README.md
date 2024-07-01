<h1 align="center">FIAP - Transfer.Microsservices - TECH CHALLENGE 3</h1>
<br>

<h2 align="center">Projeto Transfer.Microsservices</h3>

<p>Trabalho realizando durante a Pós-Graduação da FIAP de Arquitetura de Sistemas .NET + Azure</p>
<h3>Proposta: </h3>
<p>Criar uma microserviço com mensageria.</p>
<h3>O que foi desenvolvido:</h3>
<p>Foi desenvolvido 3 API's em .NET 8 com bancos separados em MongoDB para gerenciar transferência de valores monetários parecido com o PIX.</p>
<li>API com dados do usuário e valor em carteira.</li>
<li>API para realizar transferências</li>
<li>API para notificações</li>
<h3>O que foi usado?</h3>
<li>3 API's em .NET 8</li>
<li>RabbitMQ</li>
<li>MongoDB</li>
<li>Refit</li>
<li>Docker</li>

<h3 align="center">Para poder rodar o projeto</h3>
<br>

<p>Você pode rodar este projeto via Docker Compose, executando o comando abaixo na pasta principal do projeto:</p>
 <pre> docker compose up</pre>
<br>

<p>Depois de executado deverá ficar assim:</p>
<p><img src="https://github.com/tiagorv0/Transfer.Microsservices/blob/main/images/Screenshot%202024-07-01%20120207.png" /></p>
<br>
<p>Ao rodar o projeto deve-se criar duas contas para realizar as transferências:</p>
<pre>api/account/create-account</pre>
<p>Crie os usuários com nome, transferKey e saldo da conta</p>
<p>TransferKey pode ser CPF, telefone ou email</p>

<p>Depois vá na rota:</p>
<pre>/api/transfer/create-transfer</pre>
<p>Insira a TransferKey de quem vai enviar(Sender) o valor e a TransferKey de quem vai receber(Receiver)</p>
<p>ScheduleDate pode deixar nulo</p>

<p><img src="https://github.com/tiagorv0/Transfer.Microsservices/blob/main/images/Screenshot%202024-03-18%20194534.png" /></p>
<br>
<p><img src="https://github.com/tiagorv0/Transfer.Microsservices/blob/main/images/Screenshot%202024-03-18%20194559.png" /></p>
<br>
<p><img src="https://github.com/tiagorv0/Transfer.Microsservices/blob/main/images/Screenshot%202024-03-18%20194553.png" /></p>
