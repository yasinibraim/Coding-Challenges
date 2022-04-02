# GridVille Coding Challenge

---

<a href="https://img.shields.io/badge/C%23-%20-green"><img alt="JavaScript use" src="https://img.shields.io/badge/C%23-%20-green"></a>

## Welcome to Gridville!

In this city, civil engineers are obsessed with exactness. So obsessed, in fact, that the entire city was planned with its streets arranged into a perfect, rectangular grid!
 
![1](/GridVille/Others/1.png)

The local taxi company needs your programming skills to devise a navigator application. Essentially, the application will calculate the shortest route from a start point to a finish point. Sounds easy enough, no?

Unfortunately, the city planners ignored the principle of street hierarchy and, because of this, Gridville is often plagued by horrendous traffic jams which lead the flow into a standstill on some streets. The navigator app will also receive information about these traffic jams and must avoid them entirely when calculating the route.

The navigator app models the city grid into a 2D coordinate system*, where the lower left corner represents point (0, 0). Each increment in the X or Y direction represents the unit road length.
The start and finish points of a trip can occur anywhere in the grid but only at integer values (i.e. (3.15, 5.5) is not allowed) and cannot exceed the bounds of the grid.
The traffic jams are modeled as sequences of points that are blocked and cannot be included in any route.
The grid itself does not necessarily have to be a square, therefore dimensions such as 5 x 9 are also valid.

*Both the inputs and the outputs are in purely numeric format, the program does not have to do any visual processing or rendering.

## Examples

### 1. No traffic jams

![2](/GridVille/Others/2.png)

This is the ideal situation, when no street is affected by traffic jams. Consider the following 6x6 grid with start point (0,0) and finish point (6, 6).

The shortest trip takes 12 street length units. Note that, due to the geometry of the grid, there can be several routes for the same length. The blue and the black paths have the same length of 12, and there are also several other paths of length 12. When implementing the navigator, you can choose to output whichever shortest route you want; for example, it may be chosen at random or according to a strategy such as the fewest number of turns, it's your call. 

##### Inputs:

- Grid dimensions: 6 x 6
- Start point: (0, 0)
- Finish point: (6, 6)

##### Output:
- Shortest route length: 12
- Shortest route (blue route chosen): (0, 0), (1, 0), (2, 0), (3, 0), (4, 0), (5, 0), (6, 0),  (6, 1),  (6, 2),  (6, 3),  (6, 4),  (6, 5),  (6, 6)

### 2. Traffic jam

The picture below illustrates a situation with traffic jams denoted by red lines.

![3](/GridVille/Others/3.png) 

Because of this, the number of shortest routes to the destination may be severely restricted - in fact, there's just one in this example, depicted with the blue line.

##### Inputs:
- Grid dimensions: 6 x 6
- Start point: (0, 0)
- Finish point: (5, 5)
- Traffic jams: (0, 3), (1, 3), (2, 3), (3, 3), (4, 3), (5, 3), (5, 4), (5, 2), (5, 1) 

##### Output:
- Shortest route length: 12
- Shortest route (blue route chosen): (0, 0), (1, 0), (2, 0), (3, 0), (4, 0), (5, 0), (6, 0),  (6, 1),  (6, 2),  (6, 3),  (6, 4),  (6, 5),  (5, 5)

### 3. Traffic jam with no solution

![4](/GridVille/Others/4.png) 

This is the most unfortunate situation, in which the destination is entirely blocked by traffic jams and the navigator can't give a route that will reach the destination in reasonable time for the taxi client.

##### Inputs:
- Grid dimensions: 6 x 6
- Start point: (0, 0)
- Finish point: (4, 4)
- Traffic jams: (0, 3), (1, 3), (2, 3), (3, 3), (4, 3), (5, 3), (5, 4), (5, 5), (5, 6)

##### Output:
No solution

# Approach:

At a first glance, the intersections map might look like a matrix, due to the sugestive images. But, this is actually an undirected graph, in which each intersection represents a node and each street that conects two intersection is an edge. After this has been decided on, the problem becomes a simple path finding one. 

There are multiple valid approaches if we decide to continue on the previous graph assumption. The problem could be solved with multiple backtracking algorithms, such as Dijkstra (possible, but not needed since Dijkstra is used for weighted graphs), BFS, DFS and so on.

I decided upon using BFS, since it's the most common algorithm used for shortest path problems, where edges weight is not needed. 

Handling the traffic jams is another story. We could either delete the respective nodes/vertices or delete the connecting edges. I decided upon adding a simple check when creating the edges: if one of the ends of the probable edge is jammed, do not create the edge. Deleting the respective vertices is a little more complex, since we rely on each vertices position in the Graph to determine it's position on the matrix-like map. If we were to remove some vertices, all the following nodes will be shifted by a given number of positions to the left. This would imply storing another value for each node, representing it's offset on the matrix-like representation.
