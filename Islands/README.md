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

At a first glance, the intersections map might look like a matrix, due to the sugestive images. But, this is actually an undirected graph, in which each intersection represents a node and each street that conects two intersections is an edge. After this has been decided on, the problem becomes a simple path finding one. 

There are multiple valid approaches if we decide to continue on the previous graph assumption. The problem could be solved with multiple algorithms, such as Dijkstra (possible, but not needed since Dijkstra is used for weighted graphs), BFS, DFS, or other backtracking algorithms.

I decided upon using BFS, since it's the most common algorithm used for shortest path problems, where edges weight is not needed. 

Handling the traffic jams is another story. We could either delete the respective nodes/vertices, delete the connecting edges, or assign an infinite weight to any connecting edge, in case we want to use Dijkstra. I decided upon adding a simple check when creating the edges: if one of the ends of the probable edge is jammed, do not create the edge. I avoided messing with vertices, sinces deleting any nodes is a little more complex, since we rely on each vertices position in the Graph to determine it's position on the matrix-like map. If we were to remove some vertices, all the following nodes will be shifted by a given number of positions to the left. This would imply storing another value for each node, representing it's offset on the matrix-like representation.
