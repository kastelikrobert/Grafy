using graphx.Graph;
using GraphX.PCL.Common.Enums;
using GraphX.PCL.Logic.Algorithms.LayoutAlgorithms;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace graphx
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Graph.Graph G1 = new Graph.Graph(4);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowGraph(int[,] AdjMatrix)
        {
            Area.SetVerticesDrag(true);
            zoomctrl.ZoomToFill();
            SetupGraph(AdjMatrix);
            Area.GenerateGraph(true, true);
        }
        private void SetupGraph(int[,] AdjMatrix)
        {
            var logicCore = new GraphLogic();

            logicCore.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.KK;
            logicCore.DefaultLayoutAlgorithmParams = logicCore.AlgorithmFactory.CreateLayoutParameters(LayoutAlgorithmTypeEnum.KK);
            ((KKLayoutParameters)logicCore.DefaultLayoutAlgorithmParams).MaxIterations = 100;

            logicCore.DefaultOverlapRemovalAlgorithm = OverlapRemovalAlgorithmTypeEnum.FSA;
            logicCore.DefaultOverlapRemovalAlgorithmParams.HorizontalGap = 80;
            logicCore.DefaultOverlapRemovalAlgorithmParams.VerticalGap = 80;

            Area.SetVerticesHighlight(true, GraphControlType.VertexAndEdge, EdgesType.All);
            Area.SetEdgesHighlight(true, GraphControlType.VertexAndEdge);

            Area.LogicCore = logicCore;
            logicCore.Graph = GenerateGraph(AdjMatrix);
        }

        private BidirectionalGraph<DataVertex, DataEdge> GenerateGraph(int[,] matrix)
        {
            var dataGraph = new GraphData();

            List<string> colours = new List<string>() { "Gray", "AntiqueWhite", "Aquamarine", "CornflowerBlue", "BurlyWood", "Cyan", "DarkSeaGreen", "Crimson", "GreenYellow", "IndianRed", "LightBlue", "LightGreen","LightPink","LightSalmon","Teal","MistyRose","PaleTurquoise","RosyBrown","Peru","SpringGreen"};

            for (int i = 0; i < G1.IncidenceMatrix.GetLength(0); i++)
            {
                var vertex = new DataVertex($"V:{i} K:{G1.Vertices[i].Color}");
                vertex.Id = i;
                vertex.Color = colours[G1.Vertices[i].Color];
                dataGraph.AddVertex(vertex);
            }

            var vlist = dataGraph.Vertices.ToList();

            for (int i = 0; i < G1.IncidenceMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < G1.IncidenceMatrix.GetLength(1); j++)
                {
                    if ((G1.IncidenceMatrix[i, j] == G1.IncidenceMatrix[j, i]) && G1.IncidenceMatrix[i, j] == 1)
                    {
                        var edge = new DataEdge(vlist[i], vlist[j]);
                        edge.Id = i;
                        edge.Color = "Black";
                        dataGraph.AddEdge(edge);
                    }
                }
            }

            Area.GenerateGraph(true, true);
            return dataGraph;
        }

        private void BtnBFS_Click(object sender, RoutedEventArgs e)
        {
            Delta.deltaP = 0.05;
            int vertices = Convert.ToInt16(sldrVertices.Value);

            int i = 0;
            while (i < vertices)
            {
                G1 = new Graph.Graph(vertices);
                while (true)
                {
                    G1.ShowIncidenceMatrix();
                    var work = G1.BreadthFirstSearch();
                    if (work) break;
                    Delta.deltaP += 0.05;
                    sldrDelta.Value = Delta.deltaP;
                    G1 = new Graph.Graph(vertices);
                }
                i++;
                ShowGraph(G1.IncidenceMatrix);
            }
        }

        private void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            int vertices = Convert.ToInt16(sldrVertices.Value);
            Delta.deltaP = sldrDelta.Value;

            G1 = new Graph.Graph(vertices);
            ShowGraph(G1.IncidenceMatrix);
        }

        private void BtnColorize_Click(object sender, RoutedEventArgs e)
        {
            G1.Colorize();
            ShowGraph(G1.IncidenceMatrix);
        }
    }
}
