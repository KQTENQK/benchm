using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace LLAb2
{
    public partial class GPU : Window
    {
        private TimeSpan _testDuration = new TimeSpan(0, 0, 30);
        private int _frames = 0;

        public event Action<int> Ended;

        public GPU()
        {
            InitializeComponent();

            DirectionalLight dl = new DirectionalLight(Colors.White, new Vector3D(-1, -1, -1));
            dl.Color = Colors.Pink;

            _light.Content = dl;

            Model3DGroup model = new Model3DGroup();

            for (float x = -2; x < 2; x += 0.35f)
            {
                for (float y = -2; y < 2; y += 0.35f)
                {
                    for (float z = -2; z < 2; z += 0.35f)
                    {
                        model.Children.Add(InstantiateGeometry(x, y, z));
                    }
                }
            }

            CompositionTarget.Rendering += (s, a) =>
            {
                ++_frames;
            };

            DispatcherTimer fpsTimer = new DispatcherTimer();
            fpsTimer.Interval = _testDuration;
            fpsTimer.Tick += (s, a) =>
            {
                Ended?.Invoke(_frames);
                Close();
            };
            fpsTimer.Start();

            _meshObjects.Content = model;

            DoubleAnimation animation = new DoubleAnimation();
            animation.To = 40 * _testDuration.Seconds;
            animation.DecelerationRatio = 1;
            animation.Duration = TimeSpan.FromSeconds(_testDuration.Seconds);
            animation.AutoReverse = false;
            AxisAngleRotation3D rotateTransform = new AxisAngleRotation3D(new Vector3D(0, 1, 1), 5);
            model.Transform = new RotateTransform3D(rotateTransform);
            rotateTransform.BeginAnimation(AxisAngleRotation3D.AngleProperty, animation);
        }

        public Model3DGroup InstantiateGeometry(float x, float y, float z)
        {
            GeometryModel3D firstModel = new GeometryModel3D();
            GeometryModel3D secondModel = new GeometryModel3D();
            GeometryModel3D thirdModel = new GeometryModel3D();
            GeometryModel3D fourthModel = new GeometryModel3D();
            GeometryModel3D fifthModel = new GeometryModel3D();
            GeometryModel3D sixthModel = new GeometryModel3D();
            GeometryModel3D seventhModel = new GeometryModel3D();
            GeometryModel3D eighthModel = new GeometryModel3D();

            MeshGeometry3D first = new MeshGeometry3D();
            Point3DCollection pointsFirst = new Point3DCollection();

            pointsFirst.Add(new Point3D(0.0 + x, 0.0 + y, 1.0 + z));
            pointsFirst.Add(new Point3D(0.0 + x, 1.0 + y, 0.0 + z));
            pointsFirst.Add(new Point3D(1.0 + x, 0.0 + y, 0.0 + z));

            first.Positions = pointsFirst;

            MeshGeometry3D second = new MeshGeometry3D();
            Point3DCollection pointsSecond = new Point3DCollection();

            pointsSecond.Add(new Point3D(1.0 + x, 0.0 + y, 0.0 + z));
            pointsSecond.Add(new Point3D(0.0 + x, 1.0 + y, 0.0 + z));
            pointsSecond.Add(new Point3D(0.0 + x, 0.0 + y, -1.0 + z));

            second.Positions = pointsSecond;

            MeshGeometry3D third = new MeshGeometry3D();
            Point3DCollection pointsThird = new Point3DCollection();

            pointsThird.Add(new Point3D(-1.0 + x, 0.0 + y, 0.0 + z));
            pointsThird.Add(new Point3D(0.0 + x, 1.0 + y, 0.0 + z));
            pointsThird.Add(new Point3D(0.0 + x, 0.0 + y, -1.0 + z));

            third.Positions = pointsThird;

            MeshGeometry3D fourth = new MeshGeometry3D();
            Point3DCollection pointsFourth = new Point3DCollection();

            pointsFourth.Add(new Point3D(0.0 + x, 0.0 + y, 1.0 + z));
            pointsFourth.Add(new Point3D(0.0 + x, 1.0 + y, 0.0 + z));
            pointsFourth.Add(new Point3D(-1.0 + x, 0.0 + y, 0.0 + z));

            fourth.Positions = pointsFourth;

            MeshGeometry3D fifth = new MeshGeometry3D();
            Point3DCollection pointsFifth = new Point3DCollection();

            pointsFifth.Add(new Point3D(0.0 + x, 0.0 + y, 1.0 + z));
            pointsFifth.Add(new Point3D(0.0 + x, -1.0 + y, 0.0 + z));
            pointsFifth.Add(new Point3D(1.0 + x, 0.0 + y, 0.0 + z));

            fifth.Positions = pointsFifth;

            MeshGeometry3D sixth = new MeshGeometry3D();
            Point3DCollection pointsSixth = new Point3DCollection();

            pointsSixth.Add(new Point3D(1.0 + x, 0.0 + y, 0.0 + z));
            pointsSixth.Add(new Point3D(0.0 + x, -1.0 + y, 0.0 + z));
            pointsSixth.Add(new Point3D(0.0 + x, 0.0 + y, -1.0 + z));

            sixth.Positions = pointsSixth;

            MeshGeometry3D seventh = new MeshGeometry3D();
            Point3DCollection pointsSeventh = new Point3DCollection();

            pointsSeventh.Add(new Point3D(-1.0 + x, 0.0 + y, 0.0 + z));
            pointsSeventh.Add(new Point3D(0.0 + x, -1.0 + y, 0.0 + z));
            pointsSeventh.Add(new Point3D(0.0 + x, 0.0 + y, -1.0 + z));

            seventh.Positions = pointsSeventh;

            MeshGeometry3D eighth = new MeshGeometry3D();
            Point3DCollection pointsEighth = new Point3DCollection();

            pointsEighth.Add(new Point3D(0.0 + x, 0.0 + y, 1.0 + z));
            pointsEighth.Add(new Point3D(0.0 + x, -1.0 + y, 0.0 + z));
            pointsEighth.Add(new Point3D(-1.0 + x, 0.0 + y, 0.0 + z));

            eighth.Positions = pointsEighth;

            Int32Collection indencies = new Int32Collection(3);

            indencies.Add(0);
            indencies.Add(2);
            indencies.Add(1);

            first.TriangleIndices = indencies;
            second.TriangleIndices = indencies;
            third.TriangleIndices = indencies;
            fourth.TriangleIndices = indencies;
            fifth.TriangleIndices = indencies;
            sixth.TriangleIndices = indencies;
            seventh.TriangleIndices = indencies;
            eighth.TriangleIndices = indencies;

            firstModel.Geometry = first;
            secondModel.Geometry = second;
            thirdModel.Geometry = third;
            fourthModel.Geometry = fourth;
            fifthModel.Geometry = fifth;
            sixthModel.Geometry = sixth;
            seventhModel.Geometry = seventh;
            eighthModel.Geometry = eighth;

            firstModel.Material = new DiffuseMaterial(Brushes.Cyan);
            secondModel.Material = new DiffuseMaterial(Brushes.Cyan);
            thirdModel.Material = new DiffuseMaterial(Brushes.Cyan);
            fourthModel.Material = new DiffuseMaterial(Brushes.Cyan);
            fifthModel.Material = new DiffuseMaterial(Brushes.Cyan);
            sixthModel.Material = new DiffuseMaterial(Brushes.Cyan);
            seventhModel.Material = new DiffuseMaterial(Brushes.Cyan);
            eighthModel.Material = new DiffuseMaterial(Brushes.Cyan);

            Model3DGroup models = new Model3DGroup();

            models.Children.Add(firstModel);
            models.Children.Add(secondModel);
            models.Children.Add(thirdModel);
            models.Children.Add(fourthModel);
            models.Children.Add(fifthModel);
            models.Children.Add(sixthModel);
            models.Children.Add(seventhModel);
            models.Children.Add(eighthModel);

            return models;
        }
    }
}
