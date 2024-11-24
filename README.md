Requisitos:

.NET 8.0 SDK ou superior.
Visual Studio.
Portas 7002 e 7009 estarem liberadas.


Como rodar esse projeto?

Descompacte o arquivo MacManager.zip para algum local do seu computador. Abra a pasta chamada 'MacManager' e rode o MacManager.sln (necessário Visual Studio).
Com a solução aberta no Visual Studio, dê um Start, ou aperte F5.
Se as portas 7002 e 7009 estiverem disponíveis, o Visual Studio deve abrir a Web API e a solução Web juntas, pois eu marquei a opção de startup múltiplo, para testarmos nosso front. Se, por alguma razão, não vier marcado, marque a opção de startup múltiplo com a Web API e Solução Web.

Use o app :).

Como testar esse projeto?

No Visual Studio, vá na aba Test e escolha a opção 'Run All', ou rodar todos em português. Também pode ser feito pelo atalho padrão CTRL + R, A.

Considerações:

Tomei a liberdade de implementar algumas regrinhas novas além das propostas, como um CRUD de produtos, um sistema de recusa e de acompanhamento de pedidos, e mais alguns detalhes IMAGINADOS perante o escopo aberto. Tomei VÁRIAS decisões baseadas apenas em didática e para mostrar o conhecimento de estruturas comuns de mercado. Então, é uma aplicação pequenina, mas com possibilidade de escalar tranquilamente.

Um ponto importante: Tentei comentar MUITO o código, então ele difere das práticas clássicas do clean code (que diz que um código bem escrito não precisa de um comentário). Mas o fiz, pois foi requisitado no teste, por didática e para que vocês entendessem um pouco da minha visão ali durante o desenvolvimento também!

O projeto usa EF In Memory, então ele sempre reiniciará quando o app parar de rodar.
Obs : Chamei de MacManager em relacao ao McDonalds, foi uma ideia que pipocou no começo e adotei-a.
Obs²: Daria pra aumentar muuuuuito a escala desse projeto, mas acho que foi o suficiente para demonstração, mas daria pra adicionar seguranca,usuarios,etc etc etc.
Sobre o Front:

Escolhi fazer ele basicamente em HTML e jQuery, apenas usando o servidor do Razor, por questão de praticidade. Sou bem rápido com o jQuery e, com o Bootstrap, consegui fazer algo apresentável para o uso da API de maneira prática e rápida. Não ficou a coisa mais linda do mundo, pois creio que o foco não é esse, mas deixei bonitinho :). Pensei em fazer em Angular, mas daí seria chato obrigar vocês a instalar o Node, Chocolatey e rodar comandos só para umas telinhas. Mas seria bacana por minha parte também.

Muito obrigado pela chance e, qualquer dúvida, entrem em contato pelos canais. Podemos discutir ideias técnicas sobre esse projeto ou outros:

E-mail: daniel.giese.rodrigues@gmail.com
LinkedIn: https://www.linkedin.com/in/danielgiese/
