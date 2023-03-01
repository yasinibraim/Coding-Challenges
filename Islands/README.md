# Islands Coding Challenge

---

Arhipelagul insular Islands este format din mai multe insule. Putem reprezenta harta arhipelagului sub forma unui carioaj de dimensiune N x M (N linii si M coloane) cu elemente din multimea { 0, 1 } astfel: 

a. Un element egal cu 0 reprezinta apa 
b. Un element egal cu 1 reprezinta pamant 

Numim insula, o bucata de pamant formata din elemente vecine. 

Doua elemente sunt vecine daca au aceiasi valoare si sunt consecutive fie pe linie, fie pe coloana (N,S,E,V), sau au aceiasi valoare si exista un drum de la a la b ce trece numai prin elemente vecine(a vecin cu b <=> a = b, b este situat la N, S, E, V de a sau exista dab un drum de la a la b, unde dab = { dij | dij este un element vecin}).

O insula este definita ca o zona de pamant formata din elemente vecine.

O laguna este definita ca o zona, de elemente vecine, din cadrul unei insule, formata din apa, ce este inconjurata de pamant.

Data fiind harta arhipelagului sa se determine 

a) numarul de insule din arhipelag
b) suprafata celei mai mari insule 
c) suprafata celei mai mari lagune

 

E.g 

Pentru N = 5, M = 5

 

0 0 1 1 1 

0 1 0 0 0

0 0 1 1 1

0 0 1 0 1

0 0 1 1 1

 

Numarul de insule = 3

Suprafata celei mai mari insule = 8

Suprafata celei mai mari lagune = 1

 

Pentru N = 5 si M = 5

 

0 0 1 1 1 

1 1 1 0 0

0 0 0 1 1

1 1 1 1 1

1 0 1 0 0

1 1 1 0 1

 

Numarul de insule = 3

Suprafata celei mai mari insule = 12

Suprafata celei mai mari lagune = 2

# Approach:

This is a simple DFS exercise. I've implemented one DFS search for finding out the number of islands and one for the lagoons.