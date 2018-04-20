using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructure
{
    //Adjacency Matrix: One of the easiest ways to implement a graph is to use a two-dimensional matrix. In this matrix implementation, each of the rows and columns represent a vertex in the graph. The value that is stored in the cell at the intersection of row vand columnw indicates if there is an edge from vertexv to vertex w. When two vertices are connected by an edge, we say that they are adjacent. 
    //The advantage of the adjacency matrix is that it is simple, and for small graphs it is easy to see which nodes are connected to other nodes. However, notice that most of the cells in the matrix are empty. Because most of the cells are empty we say that this matrix is “sparse.” A matrix is not a very efficient way to store sparse data. The adjacency matrix is a good implementation for a graph when the number of edges is large. Since there is one row and one column for every vertex in the graph, the number of edges required to fill the matrix is |V|2. A matrix is full when every vertex is connected to every other vertex.


    //Adjacency List: A more space-efficient way to implement a sparsely connected graph is to use an adjacency list. In an adjacency list implementation we keep a master list of all the vertices in the Graph object and then each vertex object in the graph maintains a list of the other vertices that it is connected to. In our implementation of theVertexclass we will use a dictionary rather than a list where the dictionary keys are the vertices, and the values are the weights.
    //The advantage of the adjacency list implementation is that it allows us to compactly represent a sparse graph. The adjacency list also allows us to easily find all the links that are directly connected to a particular vertex.    
    public class Graph<T> where T : IComparable
    {
        private Dictionary<T, Vertex<T>> _vertices;

        public List<T> Vertices => _vertices.Select(v => v.Key).ToList();
        private int _count;

        public int NumOfVert => _count;
        public Graph()
        {
            _vertices = new Dictionary<T, Vertex<T>>();
            _count = 0;
        }

        /// <summary>
        /// adds an instance of Vertex to the graph.
        /// </summary>
        public void AddVertex(T vertKey)
        {
            _vertices.Add(vertKey, new Vertex<T>(vertKey));
            _count++;
        }

        /// <summary>
        /// Adds a new, weighted, directed edge to the graph that connects two vertices.
        /// </summary>
        /// /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="weight"></param>
        public void AddEdge(T source, T destination, int weight)
        {
            if (!_vertices.ContainsKey(source)) AddVertex(source);
            if (!_vertices.ContainsKey(destination)) AddVertex(destination);
            _vertices[source].AddNeighbor(_vertices[destination], weight);
        }

        public void AddEdge(T source, T destination)
        {
            AddEdge(source, destination, 0);
        }

        /// <summary>
        /// finds the vertex in the graph named vertKey.
        /// </summary>
        /// <param name="vertKey"></param>
        /// <returns></returns>
        public Vertex<T> GetVertex(T vertKey)
        {
            return _vertices.ContainsKey(vertKey) ? _vertices[vertKey] : null;
        }

        public bool Contains(T vertKey)
        {
            return _vertices.ContainsKey(vertKey);
        }

        #region Iterator

        public IEnumerator<Vertex<T>> GetEnumerator()
        {
            foreach (var keyValuePair in _vertices)
            {
                yield return keyValuePair.Value;
            }
        }
        #endregion

        #region Depth-First and Breadth-First Search
        /// <summary>
        /// Takes a starting point to find all vertices that can be reached by the starting vertex. Return all reacheable vertice from the start vertex.
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public IEnumerable<Vertex<T>> DFS_Visit(T start)
        {
            var startVert = GetVertex(start);
            // To make sure the depth-first search algorithm doesn't re-visit vertices, the visited HashSet keeps track of vertices already visited.
            var visited = new HashSet<Vertex<T>>();
            //A Stack, keeps track of vertices found but not yet visited. 
            var stack = new System.Collections.Generic.Stack<Vertex<T>>();
            //Initially stack contains the starting vertex.
            stack.Push(startVert);

            while (stack.Count != 0)
            {
                // The next vertex is popped from stack.
                var currentVert = stack.Pop();
                // If it has already been visited, it is discarded and the next vertex is popped from stack(continue). If it has not been visited, it is added to the set of visited vertices
                if (!visited.Add(currentVert)) continue;

                //its unvisited neighbors are added to stack. 
                var unvisitNbr = currentVert.GetConnections().Where(n => !visited.Contains(n));
                //visit order is from left to right, if not care about the order remoce Reverse()
                foreach (var next in unvisitNbr.Reverse())
                {
                    stack.Push(next);
                }
            }

            return visited;
        }

        /// <summary>
        /// BFS provides us with the ability to return the same results as DFS but with the added guarantee to return the shortest-path first, based on number of edges being the cost factor. This algorithm us queue instead of stack.
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public IEnumerable<Vertex<T>> BFS_Visit(T start)
        {
            var startVert = GetVertex(start);
            var visited = new HashSet<Vertex<T>>();
            //change stack to queue 
            var queue = new System.Collections.Generic.Queue<Vertex<T>>();
            queue.Enqueue(startVert);

            while (queue.Count != 0)
            {
                var currentVert = queue.Dequeue();
                if (!visited.Add(currentVert)) continue;
                var unvisitNbr = currentVert.GetConnections().Where(n => !visited.Contains(n));
                foreach (var next in unvisitNbr.Reverse())
                {
                    queue.Enqueue(next);
                }
            }
            return visited;
        }

        #endregion

        #region Using DFS and BFS to find path

        public IEnumerable<List<Vertex<T>>> DFS_FindPath(T start, T goal)
        {
            var startVert = GetVertex(start);
            //if want to use BFS to FindPath, use queue instead of stack here
            var stack = new System.Collections.Generic.Stack<Tuple<Vertex<T>, List<Vertex<T>>>>();
            stack.Push(new Tuple<Vertex<T>, List<Vertex<T>>>(startVert, new List<Vertex<T>>() { startVert }));

            //use a list to store found path
            var list = new List<List<Vertex<T>>>();

            while (stack.Count > 0)
            {
                var item = stack.Pop();
                var currentVert = item.Item1;
                var nbr = currentVert.GetConnections();
                foreach (var vertex in nbr)
                {
                    var path = new List<Vertex<T>>();
                    path.AddRange(item.Item2);

                    //only visite not visited nbrs
                    if (path.Contains(vertex)) continue;

                    path.Add(vertex);
                    //if found the target add path to the return list
                    if (vertex.Id.CompareTo(goal) == 0) list.Add(path);
                    //continuously push nbr to the stack
                    stack.Push(new Tuple<Vertex<T>, List<Vertex<T>>>(vertex, path));
                }
            }
            return list;

        }

        #endregion

    }
    #region Vertex Class
    public class Vertex<T>
    {
        //Edge representation: Each Vertex uses a dictionary to keep track of the vertices to which it is connected, and the weight of each edge.
        private Dictionary<Vertex<T>, int> _connectedTo;

        public T Id { get; }
        public Vertex(T key)
        {
            Id = key;
            _connectedTo = new Dictionary<Vertex<T>, int>();
        }

        /// <summary>
        /// Add a connection from this vertex to another. 
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="weight"></param>
        public void AddNeighbor(Vertex<T> vertex, int weight)
        {
            _connectedTo.Add(vertex, weight);
        }

        /// <summary>
        /// Returns all of the vertices in the adjacency list, as represented by the connectedTo instance variable.
        /// </summary>
        /// <returns></returns>
        public List<Vertex<T>> GetConnections()
        {
            return new List<Vertex<T>>(_connectedTo.Keys);
        }

        /// <summary>
        /// Returns the weight of the edge from this vertex to the vertex passed as a parameter.
        /// </summary>
        /// <param name="neighborVertex"></param>
        /// <returns></returns>
        public int GetWeight(Vertex<T> neighborVertex)
        {
            return _connectedTo[neighborVertex];
        }
    }
    #endregion



}