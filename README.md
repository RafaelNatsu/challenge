#  Challenge  

Este projeto foi idealizado para completar o desafio proposto e adicionar possíveis "facilitadores"/funções/melhorias. É o primeiro projeto sério que fiz utilizando estas tecnologias, sendo assim tornando-se um projeto extremamente trabalhoso, porém de mesma forma, cativante.  
  
  
  
A escolha das tecnologias utilizadas foi motivada majoritariamente pela minha preferência, entre tanto outro motivo que foi levado  tambem  em consideração, é o fato das mesmas terem uma presença notória no mercado de trabalho.  
  
  
  
#  Tasks  
  
O objetivo deste  challenge  é desenvolver uma aplicação que nos permita obter uma lista  de  
  
IPs  de redes  Tor  (https://www.torproject.org/) a partir de fontes externas, distintas e apresentá-los de maneira unificada. Adicionalmente esta aplicação deve possibilitar a  
  
indicação  de  IPs  de redes que NÃO  queremos  que apareçam na lista.  
  
O objetivo é desenvolver uma  API  REST  que tenha os métodos detalhados a seguir:  
  
1) Um  endpoint  GET  que devolve todos os  IPs  de  TOR  obtidos das fontes externas  
  
detalhadas  abaixo:  
  
●  https://www.dan.me.uk/tornodes  
  
●  https://onionoo.torproject.org/summary?limit=5000  
  
2) Um  endpoint  POST  que receba um  IP  e o agregue à uma base de dados onde  se  
  
encontram  todos os  IPs  que não queremos que apareçam no output do  endpoint  3  
  
(detalhado abaixo).  
  
3) Um  endpoint  GET  que devolve os  IPs  obtidos das fontes externas EXCETO os  que  
  
se  encontram na base de dados (IPs  carregados utilizando o  endpoint  2)  
  
A base de dados a ser utilizada fica à sua escolha.  
  
A aplicação desenvolvida deve executar em um  container  de  Docker.  
  
- Entregáveis:  
  
Documentação da aplicação  
  
Link  do projeto no  GitHub  com instrução de uso  
  
Demonstração da aplicação  
  
# Executando o projeto
Para executar, primeiro navegue até a pasta tools/getSiteFile/ e execute:
### `docker build -t tools-image -f Dockerfile .`
### `docker create --name tools-container tools-image`
### `docker start tools-container`
o processo demora um pouco, pois é feito a contrução dos comandos para serem executados no banco de dados.
### Importante: Execute apenas 1 vez num tempo de 30min.
Ao terminar vá para raiz e execute
### `docker-compose up -d`


https://github.com/sitepoint-editors/ToggleSwitch
https://github.com/danileao/pagination
https://github.com/do-community/build-react-pagination-demo