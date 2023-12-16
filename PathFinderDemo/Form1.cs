using MindFusion.Animations;
using MindFusion.Diagramming;
using MindFusion.Diagramming.Layout;
using MindFusion.Diagramming.Layout.Fluent;
using MindFusion.Diagramming.WinForms;
using MindFusion.Drawing;
using static System.Windows.Forms.Design.AxImporter;
using System;
using MindFusion.Diagramming.Animations;
using MindFusion.Diagramming.Fluent;
using System.Reflection;
using System.Windows.Forms;
using System.Timers;
using System.Threading;
using Microsoft.VisualBasic;

namespace PathFinderDemo
{
    public partial class Form1 : Form
    {
        Diagram diagram;

        MindFusion.Drawing.SolidBrush nodeBrush = new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 191, 12));
        MindFusion.Drawing.SolidBrush selectedNodeBrush = new MindFusion.Drawing.SolidBrush(Color.FromArgb(115, 102, 255));

        int selectedNodeCount = 0;
        ShapeNode fromNode = null;
        ShapeNode toNode = null;
        ShapeNode labelNode;

        List<List<ShapeNode>> stackOFNodes = new List<List<ShapeNode>>();

        List<ShapeNode> nodes = new List<ShapeNode>();
        int nodeCount = 0;
        char n_Label = 'A';
        public Form1()
        {
            InitializeComponent();

            //the DiagramView renders a diagram
            DiagramView diagramView = new DiagramView();
            diagramView.Dock = DockStyle.Fill;
            diagramView.Diagram.BackBrush =
                new MindFusion.Drawing.SolidBrush(Color.FromArgb(250, 247, 234));
            //change this to enable interactive creation of diagram items
            diagramView.Behavior = Behavior.Modify;

            Controls.Add(diagramView);

            diagram = diagramView.Diagram;

            //styles shape nodes
            Theme theme = new Theme();
            ShapeNodeStyle style = new ShapeNodeStyle();
            style.Brush = nodeBrush;
            style.Stroke = new MindFusion.Drawing.SolidBrush(Color.FromArgb(255, 228, 153));
            style.ShadowBrush = new MindFusion.Drawing.SolidBrush(Color.FromArgb(100, 190, 190, 190));
            theme.RegisterStyle(typeof(ShapeNode), style);
            //styles links
            DiagramLinkStyle linkStyle = new DiagramLinkStyle();
            linkStyle.Stroke = new MindFusion.Drawing.SolidBrush(Color.FromArgb(59, 170, 59));
            linkStyle.HeadStroke = new MindFusion.Drawing.SolidBrush(Color.FromArgb(59, 170, 59));
            linkStyle.Brush = new MindFusion.Drawing.SolidBrush(Color.FromArgb(161, 222, 161));
            theme.RegisterStyle(typeof(DiagramLink), linkStyle);

            diagram.Theme = theme;
            //leaves a small shadow for diagram items
            diagram.ShadowOffsetX = 0.4F;
            diagram.ShadowOffsetY = 0.4F;


            //CreateNodesAndLinks();
            ApplyLayeredLayout();

            //transparent node for the label
            CreateLabelNode();
            labelNode.Text = "The path finding algorithm is triggered when you select exactly two nodes.";
        }

        private void AddToStack(int index, ShapeNode node)
        {
            if (index < stackOFNodes.Count)
            {
                List<ShapeNode> stack = stackOFNodes[index];
                stack.Add(node);
            }
            else
            {
                List<ShapeNode> stack = new List<ShapeNode>();
                stack.Add(node);

                stackOFNodes.Add(stack);
            }
        }

        //trigger animation of a radio button has been checked/unchecked 
        //and the selected nodes are 2

        private void CreateLabelNode()
        {
            labelNode = diagram.Factory.CreateShapeNode(100, 3, 150, 5);
            labelNode.IgnoreLayout = true;
            labelNode.Locked = true;
            labelNode.Transparent = true;
            labelNode.Font = new Font("Verdana", 10);
            labelNode.Text = "";
        }

        private void Node_ToSearchClicked(object? sender, NodeEventArgs e)
        {
            labelNode.Text = "";

            ShapeNode node = e.Node as ShapeNode;

            if (node != null)
            {
                node.Brush = selectedNodeBrush;
                selectedNodeCount++;

                fromNode = nodes[0];
                toNode = node;
                AnimateGraph();
            }
        }

        //event handler for the nodeClicked event 
        private void Diagram_NodeClicked(object? sender, NodeEventArgs e)
        {
            labelNode.Text = "";

            ShapeNode node = e.Node as ShapeNode;

            if (node != null)
            {
                node.Brush = selectedNodeBrush;
                selectedNodeCount++;

                if (selectedNodeCount == 1)
                    fromNode = node;
                else if (selectedNodeCount == 2)
                {
                    toNode = node;
                    //AnimateGraph();
                    var link = diagram.Factory.CreateDiagramLink(fromNode, toNode);
                    link.HeadShape = ArrowHeads.None;

                    if (weight_TB.Text != string.Empty)
                        link.Text = weight_TB.Text;
                    else
                        link.Text = "1";


                    /*link = diagram.Factory.CreateDiagramLink(toNode, fromNode);
                    link.HeadShape = ArrowHeads.BowArrow;

                    link.Text = weight_TB.Text;*/

                    fromNode.Brush = nodeBrush;
                    toNode.Brush = nodeBrush;



                    fromNode = null;
                    toNode = null;

                    selectedNodeCount = 0;
                }
            }

        }


        //finds a path between the two nodes selected and triggers the animation
        private void AnimateGraph()
        {
            if (fromNode != null && toNode != null)
            {
                PathFinder pathFinder = new PathFinder(diagram);
                MindFusion.Diagramming.Path path;

                path = pathFinder.FindShortestPath(fromNode, toNode);

                if (path != null && path.Nodes.Count > 1)
                {
                    RunAnimation(path);
                }
                else
                    labelNode.Text = "No path found";
            }
        }

        //runs a path animation between the two nodes selected
        private void RunAnimation(MindFusion.Diagramming.Path path)
        {
            List<PointF> animationPoints = new List<PointF>();
            if (path.Items.Count == 0)
                return;

            foreach (DiagramItem item in path.Items)
            {
                if (item is DiagramLink)
                {
                    DiagramLink link = item as DiagramLink;

                    foreach (PointF point in link.ControlPoints)
                        animationPoints.Add(point);
                }
            }

            ShapeNode dummy = diagram.Factory.CreateShapeNode(animationPoints[0].X - 2, animationPoints[0].Y - 2, 4, 4);
            dummy.Shape = Shapes.Ellipse;
            dummy.Brush = new MindFusion.Drawing.SolidBrush(Color.FromArgb(108, 0, 0));

            PathAnimation animation = new PathAnimation(
             animationPoints,
             new AnimationOptions
             {
                 Duration = 500 * path.Items.Count,
                 KeepLastValue = true
             });


            dummy.Animate(animation);
            animation.Start();

            animation.AnimationComplete += (s, e) =>
            {
                diagram.Items.Remove(dummy);

            };

            toNode.Brush = nodeBrush;
        }

        //creates a new random graph
        private void Button1_Click(object? sender, EventArgs e)
        {
            ApplyLayeredLayout();
            CreateLabelNode();

            toNode = null;
            fromNode = null;
            selectedNodeCount = 0;
        }

        Random random = new Random(42);

        //arranges the graph using the LayeredLayout and adjusts some of its properties
        private void ApplyLayeredLayout()
        {
            var layout = new LayeredLayout();
            layout.Margins = new Size(20, 20);
            layout.Direction = Direction.Straight;
            layout.Orientation = MindFusion.Diagramming.Layout.Orientation.Vertical;
            layout.NodeDistance = 30;
            layout.LayerDistance = 18;
            layout.StraightenLongLinks = true;
            layout.SwapPairsIterations = 5;

            diagram.Arrange(layout);
            diagram.ResizeToFitItems(1.0f);
        }

        private void addNode_Bttn_Click(object sender, EventArgs e)
        {
            var node = diagram.Factory.CreateShapeNode(10, 10, 10, 10);
            node.Text = n_Label.ToString();
            n_Label++;
            nodeCount++;

            nodes.Add(node);

            ApplyLayeredLayout();  // Apply the layered layout
            CreateLabelNode();
        }

        private void connectBx_CheckedChanged(object sender, EventArgs e)
        {
            //raised when nodes are clicked
            if (connectBx.Checked)
                diagram.NodeClicked += Diagram_NodeClicked;
            else
            {
                diagram.NodeClicked -= Diagram_NodeClicked;
            }
        }

        private void searchNode_bttn_Click(object sender, EventArgs e)
        {
            //AnimateGraph();
            int searchNode = int.Parse(search_TB.Text);
            ShapeNode fNode = nodes[0];
            ShapeNode tNode = nodes[searchNode];
            MessageBox.Show("Root: " + fNode.Text);
            MessageBox.Show("SearchNode: " + tNode.Text);
            if (fromNode != null && toNode != null)
            {
                PathFinder pathFinder = new PathFinder(diagram);
                MindFusion.Diagramming.Path path;

                path = pathFinder.FindShortestPath(fNode, tNode);

                if (path != null && path.Nodes.Count > 1)
                    RunAnimation(path);
                else
                    labelNode.Text = "No path found";
            }
        }



        private void search_CB_CheckedChanged(object sender, EventArgs e)
        {
            if (search_CB.Checked)
                diagram.NodeClicked += Node_ToSearchClicked;
            else
                diagram.NodeClicked -= Node_ToSearchClicked;
        }

        /// 
        /// 
        /// 

        //  Functions for Searching Algorithms
        private bool isPaused = false; // User for the Timer in all Searching Algorithm
        private void PauseOrContinue(System.Windows.Forms.Timer timer)
        {
            pause_bttn.Click += (s, e) =>
            {
                isPaused = !isPaused;
                if (isPaused)
                {
                    timer.Stop();
                    pause_bttn.Text = "Play";
                }
                else
                {
                    timer.Start();
                    pause_bttn.Text = "Pause";
                }
            };
        }

        private int baseInterval = 500; // Initial interval in milliseconds
        private int currentInterval;

        // DFS Algorithm ///////////////////////////////////////
        private void VisualizeDFSAndAnimate(ShapeNode startNode)
        {
            // Reset node colors
            foreach (var node in nodes)
            {
                node.Brush = nodeBrush;
            }

            // Create a stack for DFS
            Stack<ShapeNode> stack = new Stack<ShapeNode>();
            Stack<ShapeNode> mystack = new Stack<ShapeNode>();

            List<ShapeNode> visitedNode = new List<ShapeNode>();
            stack.Push(startNode);

            currentInterval = baseInterval;

            currentInterval = baseInterval;
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = currentInterval;
            int delayCount = 0;


            ShapeNode prevNode = startNode;
            timer.Tick += (s, e) =>
            {
                prevNode.Brush = nodeBrush;
                if (stack.Count > 0)
                {
                    ShapeNode current = stack.Pop();

                    if (!visitedNode.Contains(current))
                    {
                        mystack.Push(current);
                        visitedNode.Add(current);
                    }



                    // Process the current node
                    current.Brush = selectedNodeBrush;
                    prevNode = current;
                    // Get adjacent nodes in a left-to-right order
                    List<ShapeNode> neighbors = GetNeighborsLeftToRight(current);



                    stackLBL.Text += current.Text + " = ";

                    ShapeNode targetNode = nodes.FirstOrDefault(node => node.Text == search_TB.Text);
                    if (visitedNode.Contains(targetNode))
                    {
                        stackLBL.Text += "\n\n";
                        foreach (var n in visitedNode)
                            stackLBL.Text += n.Text + " , ";
                        timer.Stop();
                    }


                    foreach (var n in neighbors)
                    {
                        if (!visitedNode.Contains(n))
                            stackLBL.Text += n.Text + " , ";
                        //n.Brush = selectedNodeBrush;
                    }
                    stackLBL.Text += "\n";

                    // Push unvisited neighbors onto the stack in a left-to-right order
                    foreach (var neighbor in neighbors)
                    {
                        if (!mystack.Contains(neighbor))
                        {
                            stack.Push(neighbor);
                        }
                    }

                    delayCount++;

                    if (delayCount == nodes.Count) // Adjust the condition based on your requirements
                    {
                        timer.Stop();
                        /*foreach (var n in visitedNode)
                            stackLBL.Text += n.Text + " , ";*/
                    }
                }
            };

            PauseOrContinue(timer);

            speedUp_bttn.Click += (s, e) =>
            {
                int spdIntrvl = int.Parse(Speed_Disp.Text);
                if (spdIntrvl < 20)
                {
                    spdIntrvl++;

                    currentInterval = Math.Max(50, currentInterval - 100); // Adjust the decrement value based on your needs
                    timer.Interval = currentInterval;


                    Speed_Disp.Text = spdIntrvl.ToString();
                }
            };

            speedDown_bttn.Click += (s, e) =>
            {
                int spdIntrvl = int.Parse(Speed_Disp.Text);
                if (spdIntrvl > 1)
                {
                    spdIntrvl--;

                    currentInterval += 100; // Adjust the increment value based on your needs
                    timer.Interval = currentInterval;

                    Speed_Disp.Text = spdIntrvl.ToString();
                }
            };

            timer.Start();
        }


        private List<ShapeNode> GetNeighborsLeftToRight(ShapeNode node)
        {
            List<ShapeNode> neighbors = new List<ShapeNode>();

            foreach (var link in diagram.Links)
            {
                if (link.Origin == node)
                {
                    neighbors.Insert(0, link.Destination as ShapeNode); // Insert at the beginning for left-to-right order
                }
                else if (link.Destination == node)
                {
                    neighbors.Add(link.Origin as ShapeNode);
                }
            }

            return neighbors;
        }
        /////////////////////////////////////////////////////////////

        // BFS Algorithm 
        private void VisualizeBFSAndAnimate(ShapeNode startNode)
        {
            // Reset node colors
            foreach (var node in nodes)
            {
                node.Brush = nodeBrush;
            }

            // Create a queue for BFS
            Queue<ShapeNode> queue = new Queue<ShapeNode>();

            List<ShapeNode> visitedNode = new List<ShapeNode>();

            queue.Enqueue(startNode);
            visitedNode.Add(startNode);

            currentInterval = baseInterval;

            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = currentInterval;
            int nodesAtCurrentLevel = 1; // Number of nodes at the current level

            timer.Tick += (s, e) =>
            {
                if (nodesAtCurrentLevel > 0)
                {
                    ShapeNode current = queue.Dequeue();

                    // Process the current node
                    current.Brush = selectedNodeBrush;

                    // Get adjacent nodes
                    List<ShapeNode> neighbors = GetNeighborsLeftToRight_BFS(current);

                    stackLBL.Text += current.Text + "=";
                    foreach (var n in neighbors)
                    {
                        if (!visitedNode.Contains(n))
                        {
                            visitedNode.Add(n);
                            queue.Enqueue(n);
                            stackLBL.Text += n.Text + " , ";
                        }
                    }
                    stackLBL.Text += "\n";

                    nodesAtCurrentLevel--;

                    if (nodesAtCurrentLevel == 0)
                    {
                        stackLBL.Text += "\n"; // Move to the next line for the next level
                                               // Move to the next level
                        nodesAtCurrentLevel = queue.Count;
                    }

                    // Process child nodes of the current level
                    /*foreach (var n in neighbors)
                    {
                        n.Brush = selectedNodeBrush;
                        if (!visitedNode.Contains(n))
                        {
                            visitedNode.Add(n);
                            n.Brush = selectedNodeBrush;
                            stackLBL.Text += n.Text + " , ";
                        }
                    }*/

                    ShapeNode targetNode = nodes.FirstOrDefault(node => node.Text == search_TB.Text);
                    if (visitedNode.Contains(targetNode))
                    {
                        foreach (var n in visitedNode)
                            stackLBL.Text += n.Text + " , ";
                        timer.Stop();
                    }
                }
            };

            PauseOrContinue(timer);

            speedUp_bttn.Click += (s, e) =>
            {
                int spdIntrvl = int.Parse(Speed_Disp.Text);
                if (spdIntrvl < 20)
                {
                    spdIntrvl++;

                    currentInterval = Math.Max(50, currentInterval - 100); // Adjust the decrement value based on your needs
                    timer.Interval = currentInterval;

                    Speed_Disp.Text = spdIntrvl.ToString();
                }
            };

            speedDown_bttn.Click += (s, e) =>
            {
                int spdIntrvl = int.Parse(Speed_Disp.Text);
                if (spdIntrvl > 1)
                {
                    spdIntrvl--;

                    currentInterval += 100; // Adjust the increment value based on your needs
                    timer.Interval = currentInterval;

                    Speed_Disp.Text = spdIntrvl.ToString();
                }
            };

            timer.Start();
        }



        private List<ShapeNode> GetNeighborsLeftToRight_BFS(ShapeNode node)
        {
            List<ShapeNode> neighbors = new List<ShapeNode>();

            foreach (var link in diagram.Links)
            {
                if (link.Origin == node)
                {
                    neighbors.Add(link.Destination as ShapeNode);
                }
                else if (link.Destination == node)
                {
                    neighbors.Add(link.Origin as ShapeNode);
                }
            }

            return neighbors;
        }
        /////////////////////////////////////////////////


        // Hill Climbing
        // Hill Climbing Algorithm
        private void VisualizeHillClimbingAndAnimate(ShapeNode startNode, ShapeNode goalNode)
        {
            // Reset node colors
            foreach (var node in nodes)
            {
                node.Brush = nodeBrush;
            }

            ShapeNode current = startNode;
            ShapeNode prevNode = null;

            current.Brush = selectedNodeBrush;

            currentInterval = baseInterval;

            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = currentInterval;

            timer.Tick += (s, e) =>
            {
                if (current != goalNode)
                {
                    double heuristicCurrent = CalculateEuclideanDistance(current, goalNode);
                    int heuristicInt = Convert.ToInt32(heuristicCurrent); // or (int)heuristic;

                    heuristics_lbl.Text += heuristicInt.ToString() + "\n";
                    // Get neighbors
                    List<ShapeNode> neighbors = GetNeighborsLeftToRight(current);

                    // Find the neighbor with the lowest heuristic (Euclidean distance to the goal)
                    double minHeuristic = double.MaxValue;
                    ShapeNode nextNode = null;


                    heuristics_lbl.Text = string.Empty;
                    foreach (var neighbor in neighbors)
                    {
                        double heuristic = CalculateEuclideanDistance(neighbor, goalNode);
                        if (heuristic == 0)
                        {
                            /* int hInt = Convert.ToInt32(heuristic);
                             heuristics_lbl.Text += hInt.ToString() + "\n";*/
                        }

                        int hInt = Convert.ToInt32(heuristic);
                        heuristics_lbl.Text += hInt.ToString() + "\n";
                        // Get neighbors

                        if (heuristic < minHeuristic)
                        {
                            minHeuristic = heuristic;
                            nextNode = neighbor;
                        }
                    }

                    if (nextNode != null)
                    {
                        prevNode = current;
                        current = nextNode;

                        current.Brush = selectedNodeBrush;
                        prevNode.Brush = nodeBrush;
                    }
                    else
                        timer.Stop();
                }
                else
                {
                    timer.Stop();
                }
            };

            PauseOrContinue(timer);

            speedUp_bttn.Click += (s, e) =>
            {
                int spdIntrvl = int.Parse(Speed_Disp.Text);
                if (spdIntrvl < 20)
                {
                    spdIntrvl++;

                    currentInterval = Math.Max(50, currentInterval - 100); // Adjust the decrement value based on your needs
                    timer.Interval = currentInterval;

                    Speed_Disp.Text = spdIntrvl.ToString();
                }
            };

            speedDown_bttn.Click += (s, e) =>
            {
                int spdIntrvl = int.Parse(Speed_Disp.Text);
                if (spdIntrvl > 1)
                {
                    spdIntrvl++;

                    currentInterval += 100; // Adjust the increment value based on your needs
                    timer.Interval = currentInterval;

                    Speed_Disp.Text = spdIntrvl.ToString();
                }
            };

            timer.Start();
        }

        private void hillClimbing_bttn_Click(object sender, EventArgs e)
        {
            // Assuming you want to start Hill Climbing from the first node to the last node
            if (nodes.Count > 1)
            {
                VisualizeHillClimbingAndAnimate(nodes[0], nodes[nodes.Count - 1]);
            }
        }

        private double CalculateEuclideanDistance(ShapeNode node1, ShapeNode node2) // Get the Distance between Two Nodes
        {
            PointF center1 = new PointF(node1.Bounds.X + node1.Bounds.Width / 2, node1.Bounds.Y + node1.Bounds.Height / 2);
            PointF center2 = new PointF(node2.Bounds.X + node2.Bounds.Width / 2, node2.Bounds.Y + node2.Bounds.Height / 2);

            float deltaX = center1.X - center2.X;
            float deltaY = center1.Y - center2.Y;

            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
        ///////////

        // Beam
        private List<Tuple<ShapeNode, double>> GetNeighborsWithCost(ShapeNode node, Dictionary<ShapeNode, double> nodeCosts)
        {
            List<Tuple<ShapeNode, double>> neighbors = new List<Tuple<ShapeNode, double>>();

            foreach (var link in diagram.Links)
            {
                ShapeNode neighborNode = null;

                if (link.Origin == node)
                {
                    neighborNode = link.Destination as ShapeNode;
                }
                else if (link.Destination == node)
                {
                    neighborNode = link.Origin as ShapeNode;
                }

                if (neighborNode != null)
                {
                    double linkWeight = GetLinkWeight(link);

                    // Calculate the total cost to reach the neighbor
                    double totalCost = nodeCosts[node] + linkWeight;

                    neighbors.Add(new Tuple<ShapeNode, double>(neighborNode, totalCost));
                }
            }

            return neighbors;
        }

        private double GetLinkWeight(DiagramLink link)
        {
            double weight;

            // Try parsing the link text as a double
            if (double.TryParse(link.Text, out weight))
            {
                return weight;
            }

            // Default weight if parsing fails
            return 1.0;
        }

        private void VisualizeBeamSearchAndAnimate(ShapeNode startNode, ShapeNode goalNode, int beamWidth)
        {
            // Reset node colors
            foreach (var node in nodes)
            {
                node.Brush = nodeBrush;
            }

            // Create a priority queue for beam search
            PriorityQueue<ShapeNode, double> beamQueue = new PriorityQueue<ShapeNode, double>();
            Dictionary<ShapeNode, double> nodeCosts = new Dictionary<ShapeNode, double>();

            // Enqueue the starting node with a cost of 0
            beamQueue.Enqueue(startNode, 0);
            nodeCosts[startNode] = 0;

            // Set the base interval for animation
            currentInterval = baseInterval;

            // Create a timer for animation
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = currentInterval;
            int delayCount = 0;

            // Keep track of the previously processed node for animation
            ShapeNode prevNode = startNode;

            // Event handler for the timer tick
            timer.Tick += (s, e) =>
            {
                // Reset the color of the previous node
                prevNode.Brush = nodeBrush;

                if (beamQueue.Count > 0)
                {
                    // Dequeue nodes based on the beam width
                    List<ShapeNode> currentNodes = new List<ShapeNode>();
                    for (int i = 0; i < beamWidth && beamQueue.Count > 0; i++)
                    {
                        var current = beamQueue.Dequeue();
                        currentNodes.Add(current);

                        // Process the current node
                        current.Brush = selectedNodeBrush;
                        prevNode = current;
                        //stackLBL.Text += current.Text + " , ";
                    }
                    //stackLBL.Text += "\n";

                    foreach (var currentNode in currentNodes)
                    {
                        // Get adjacent nodes with their costs
                        var neighbors = GetNeighborsWithCost(currentNode, nodeCosts);

                        foreach (var neighbor in neighbors)
                        {
                            // If the neighbor is not in the nodeCosts dictionary or has a lower cost, update the cost and enqueue
                            if (!nodeCosts.ContainsKey(neighbor.Item1) || neighbor.Item2 < nodeCosts[neighbor.Item1])
                            {
                                nodeCosts[neighbor.Item1] = neighbor.Item2;
                                beamQueue.Enqueue(neighbor.Item1, neighbor.Item2);
                                heuristics_lbl.Text += neighbor.Item2.ToString() + "\n";
                                stackLBL.Text += neighbor.Item1.Text + " , ";
                                neighbor.Item1.Brush = selectedNodeBrush;
                            }
                            else
                            {
                                if(neighbor.Item1 == goalNode)
                                    neighbor.Item1.Brush = selectedNodeBrush;
                                timer.Stop();
                            }
                                
                        }
                        stackLBL.Text += "\n";
                    }

                    delayCount++;

                    // Check if the animation should stop
                    if (delayCount == nodes.Count) // Adjust the condition based on your requirements
                    {
                        timer.Stop();
                        foreach (var n in nodeCosts.Keys)
                            stackLBL.Text += n.Text + " , ";
                    }
                }
            };

            // Pause or continue the animation
            PauseOrContinue(timer);

            // Speed up the animation
            speedUp_bttn.Click += (s, e) =>
            {
                int spdIntrvl = int.Parse(Speed_Disp.Text);
                if (spdIntrvl < 20)
                {
                    spdIntrvl++;

                    // Decrease the interval to speed up the animation
                    currentInterval = Math.Max(50, currentInterval - 100); // Adjust the decrement value based on your needs
                    timer.Interval = currentInterval;

                    Speed_Disp.Text = spdIntrvl.ToString();
                }
            };

            // Speed down the animation
            speedDown_bttn.Click += (s, e) =>
            {
                int spdIntrvl = int.Parse(Speed_Disp.Text);
                if (spdIntrvl > 1)
                {
                    spdIntrvl--;

                    // Increase the interval to slow down the animation
                    currentInterval += 100; // Adjust the increment value based on your needs
                    timer.Interval = currentInterval;

                    Speed_Disp.Text = spdIntrvl.ToString();
                }
            };

            // Start the timer
            timer.Start();
        }


        ///////////


        private void dfs_bttn_Click(object sender, EventArgs e)
        {
            // Assuming you want to start DFS from the first node
            if (nodes.Count > 0)
            {
                VisualizeDFSAndAnimate(nodes[0]);
            }
        }

        private void DFS_CB_CheckedChanged(object sender, EventArgs e)
        {
            pause_bttn.Text = "Pause";

            if (DFS_CB.Checked)
            {
                connectBx.Checked = false;
                search_CB.Checked = false;
                BFS_CB.Checked = false;
            }
        }

        private void searchNode_bttn_Click_1(object sender, EventArgs e)
        {

            stackLBL.Text = string.Empty;

            Speed_Disp.Text = "10";
            pause_bttn.Text = "Pause";

            if (nodes.Count > 0)
            {
                ShapeNode startNode = nodes.FirstOrDefault(node => node.Text == start_TB.Text);
                if (DFS_CB.Checked)
                    VisualizeDFSAndAnimate(startNode);
                else if (BFS_CB.Checked)
                    VisualizeBFSAndAnimate(startNode);
                else if (hillClimb_CB.Checked)
                {
                    string targetText = search_TB.Text; // Change this to your desired text
                    ShapeNode targetNode = nodes.FirstOrDefault(node => node.Text == targetText);

                    if (targetNode != null)
                    {
                        VisualizeHillClimbingAndAnimate(nodes[0], targetNode);
                        targetNode.Brush = selectedNodeBrush;
                    }

                }
                else if (beam_CB.Checked)
                {
                    string targetText = search_TB.Text; // Change this to your desired text
                    ShapeNode targetNode = nodes.FirstOrDefault(node => node.Text == targetText);

                    int w = int.Parse(w_TB.Text);

                    if (targetNode != null)
                    {
                        VisualizeBeamSearchAndAnimate(nodes[0], targetNode, w);
                        targetNode.Brush = selectedNodeBrush;
                    }

                }
                else
                    MessageBox.Show("Select Searching Algo");
            }
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            diagram.ClearAll();
            nodes = new List<ShapeNode>();
            n_Label = 'A';
            nodeCount = 0;
        }

        private void BFS_CB_CheckedChanged(object sender, EventArgs e)
        {
            pause_bttn.Text = "Pause";

            if (BFS_CB.Checked)
            {
                connectBx.Checked = false;
                search_CB.Checked = false;
                DFS_CB.Checked = false;
            }
        }
        private void pause_bttn_Click(object sender, EventArgs e)
        {
            /*if (pause_bttn.Text == "Pause")
            {
                pause_bttn.Text = "Play";
            }
            else
                pause_bttn.Text = "Pause";*/
        }

        private void hillClimb_CB_CheckedChanged(object sender, EventArgs e)
        {
            pause_bttn.Text = "Pause";

            if (hillClimb_CB.Checked)
            {
                connectBx.Checked = false;
                search_CB.Checked = false;
                DFS_CB.Checked = false;
                BFS_CB.Checked = false;
            }
        }

        private void beam_CB_CheckedChanged(object sender, EventArgs e)
        {
            pause_bttn.Text = "Pause";

            if (beam_CB.Checked)
            {
                connectBx.Checked = false;
                search_CB.Checked = false;
                DFS_CB.Checked = false;
                BFS_CB.Checked = false;
                hillClimb_CB.Checked = false;
            }
        }
    }
}