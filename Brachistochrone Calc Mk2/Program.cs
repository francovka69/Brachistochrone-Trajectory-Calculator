using Brachistochrone_Calc_Mk2.ModelClasses;
using NStack;
using System.Timers;
using Terminal.Gui;

namespace Brachistochrone_Calc_Mk2;

//franto z budoucnosti sprav pak tu chybu s interstellar kalkulacema pls!!
public class Program
{
    static void Main(string[] args)
    {
        Application.Init();
        ObjectDatabase db = FileManager.LoadObjects("Targets.csv");

        var window = new Window("Brachistochrone Calculator")
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };

        var ShipLabel = new Label("Ship (can be blank)")
        {
            X = Pos.Percent(100) - 50,
            Y = 2
        };

        var ShipInput = new TextField("")
        {
            X = Pos.Percent(100) - 50,
            Y = 3,
            Width = 20
        };

        var InfoBoxExplain = new Button("?")
        {
            X = Pos.Percent(100) - 5,
            Y = 27
        };

        var title = new Label("Brachistochrone Space Trajectory Calculator")
        {
            X = Pos.Center(),
            Y = 1
        };

        //malej panel
        var inputPanel01 = new FrameView("Acceleration")
        {
            X = 1,
            Y = 3,
            Width = 23,
            Height = 5
        };

        var inputPanel02 = new FrameView("Target")
        {
            X = 1,
            Y = 9,
            Width = 23,
            Height = 15

        };

        var PrintOutButton = new Button("Save to a File")
        {
            X = Pos.Percent(100) - 20,
            Y = 4
        };

        //vypis dat po tlacitku + tlacitko
        var infoBox = new FrameView("Flight data")
        {
            X = Pos.Center(),
            Y = 4,
            Width = 51,
            Height = 10,
            Visible = false
        };

        var calcbutton = new Button("Calculate")
        {
            X = Pos.Percent(100) - 15,
            Y = 2
        };

        var ShipGenerateButton = new Button("Generate")
        {
            X = Pos.Percent(100) - 14,
            Y = 6

        };

        //actuall data do infoboxu 
        var planetLabel = new Label("Planet: ")
        {
            X = Pos.Center(),
            Y = 2
        };

        var shiplabelinfobox = new Label("")
        {
            X = Pos.Center(),
            Y = 3
        };

        //min-------------------------------------------
        var MinDistanceLabel = new Label("Min Distance: ")
        {
            X = Pos.Percent(0) + 1,
            Y = 5
        };

        var MinTimeLabel = new Label("Time: ")
        {
            X = Pos.Percent(100) - 15,
            Y = 5
        };

        //avg-------------------------------------------
        var AvgDistanceLabel = new Label("Avg Distance: ")
        {
            X = Pos.Percent(0) + 1,
            Y = 6
        };
        
        var AvgTimeLabel = new Label("Time: ")
        {
            X = Pos.Percent(100) - 15,
            Y = 6
        };

        //max-------------------------------------------
        var MaxDistanceLabel = new Label("Max Distance: ")
        {
            X = Pos.Percent(0) + 1,
            Y = 7
        };

        var MaxTimeLabel = new Label("Time: ")
        {
            X = Pos.Percent(100) - 15,
            Y = 7
        };

        //scroll list

        List<Planet> planets = db.Objects;

        string[] names = planets.Select(p => p.Name).ToArray();

        var radio = new RadioGroup()
        {
            X = Pos.Center(),
            Y = 1,
            RadioLabels = names.Select(n => (NStack.ustring)n).ToArray()
        };



        double gForce = 1;

        var gLabel = new Label($"G: {gForce:F1}")
        {
            X = 2,
            Y = 1
        };

        var dInput = new TextField("")
        {
            X = 2,
            Y = 2,
            Width = 17
        };

        dInput.ColorScheme = new ColorScheme()
        {
            Normal = Application.Driver.MakeAttribute(Color.White, Color.DarkGray),
            Focus = Application.Driver.MakeAttribute(Color.Black, Color.DarkGray)
        };


        var minus = new Button("-")
        {
            X = 10,
            Y = 1
        };

        var plus = new Button("+")
        {
            X = 14,
            Y = 1
        };

        //varovani
        var warningLabel = new Label("")
        {
            X = Pos.Center(),
            Y = 26
        };


        Application.Top.ColorScheme = new ColorScheme()
        {
            Normal = Application.Driver.MakeAttribute(Color.White, Color.Black)
        };

        window.ColorScheme = new ColorScheme()
        {
            Normal = Application.Driver.MakeAttribute(Color.White, Color.Black)
        };

        //varovani logika + prebarvovani cehokoli + upraveni vlastnosti panelu
        void UpdateUI()
        {
            gLabel.Text = $"G: {gForce:F1}";

            if (gForce > 3)
            {
                warningLabel.Text = "⚠ CRITICAL: high risk of injury";
                warningLabel.ColorScheme = new ColorScheme()
                {
                    Normal = Application.Driver.MakeAttribute(Color.White, Color.BrightRed)
                };
            }
            else if (gForce > 1.4)
            {
                warningLabel.Text = "Higher G can injure you over time";
                warningLabel.ColorScheme = new ColorScheme()
                {
                    Normal = Application.Driver.MakeAttribute(Color.Black, Color.BrightYellow)
                };
            }
            else
            {
                warningLabel.Text = "";
            }
        }

        ShipInput.ColorScheme = new ColorScheme()
        {
            Normal = Application.Driver.MakeAttribute(
                Color.Black,
                Color.White),

            Focus = Application.Driver.MakeAttribute(
                Color.Black,
                Color.White)
        };

        infoBox.ColorScheme = new ColorScheme()
        {
            Normal = Application.Driver.MakeAttribute(Color.White, Color.Blue),
            Focus = Application.Driver.MakeAttribute(Color.Black, Color.BrightCyan)
        };

        infoBox.Border.BorderStyle = BorderStyle.Double;


        //extremni mechanismus :o ----------------------
        calcbutton.Clicked += () =>
        {
            string userText = ShipInput.Text.ToString().Trim();
            var selected = planets[radio.SelectedItem];

            Calculator Calc = new Calculator(
                gForce,
                selected.Distances.Min,
                selected.Distances.Avg,
                selected.Distances.Max
            );

            Calc.ActualCalc();

            infoBox.Visible = !infoBox.Visible;

            calcbutton.Text = infoBox.Visible
                ? "Hide"
                : "Calculate";

            shiplabelinfobox.Text = $"Ship: {userText}";
            planetLabel.Text = $"Planet: {selected.Name}";

            MinDistanceLabel.Text = $"Min Distance (km): {selected.Distances.Min}";
            AvgDistanceLabel.Text = $"Avg Distance (km): {selected.Distances.Avg}";
            MaxDistanceLabel.Text = $"Max Distance (km): {selected.Distances.Max}";

            MinTimeLabel.Text = $"Time: {Calc.ResultMinT:F2} Days";
            AvgTimeLabel.Text = $"Time: {Calc.ResultAvgT:F2} Days";
            MaxTimeLabel.Text = $"Time: {Calc.ResultMaxT:F2} Days";
        };

        //----------------------------------------------
        //khaby lame mechanismy-------------------------

        PrintOutButton.Clicked += () =>
        {
            string userText = ShipInput.Text.ToString().Trim();

            var selected = planets[radio.SelectedItem];

            Calculator CalcFinal = new Calculator(
                gForce,
                selected.Distances.Min,
                selected.Distances.Avg,
                selected.Distances.Max
            );

            CalcFinal.ActualCalc();

            OverviewPrintout Final = new OverviewPrintout(
                selected.Distances.Min,
                selected.Distances.Avg,
                selected.Distances.Max,
                CalcFinal.ResultMinT,
                CalcFinal.ResultAvgT,
                CalcFinal.ResultMaxT,
                userText,
                selected.Name
            );

            Final.Ship = userText;

            Final.Printout("Flight_Summary.txt");

            MessageBox.Query("Status", "Done! saved to the Flight_Summary.txt", "Ok");
        };

        minus.Clicked += () =>
        {
            if (gForce > 0.1)
                gForce -= 0.1;

            UpdateUI();
        };

        plus.Clicked += () =>
        {
            if (gForce < 10)
                gForce += 0.1;

            UpdateUI();
        };

        InfoBoxExplain.Clicked += () =>
        {
            MessageBox.Query("Explanation", "A brachistochrone trajectory is a trajectory of constant acceleratio" +
                "n. If ship accelerates under a constant G the feeling would be indistinguishable from gravity. " +
                "Its commonly used in sci-fi show The Expanse, also where i found this method of transport interesting " +
                "Unfortunately we cannot use this method of transportation yet as it uses tremendous amounts " +
                "of fuel + we dont have an engine that can burn for days on end without breaking / melting. " +
                "In the show they solve this by developing a Fusion drive that has the unfortunate name of " +
                "Epstein Drive", "OK");
        };

        ShipGenerateButton.Clicked += () =>
        {
            Ship GenName = new Ship();
            GenName.Generate();

            ShipInput.Text = GenName.Name;
        };

        //vykresleni 

        inputPanel01.Add(gLabel, minus, plus);
        inputPanel02.Add(radio);
        infoBox.Add(planetLabel, MinDistanceLabel, MinTimeLabel, AvgDistanceLabel, AvgTimeLabel, MaxDistanceLabel, MaxTimeLabel, shiplabelinfobox);
        window.Add(title, inputPanel01, inputPanel02, warningLabel, calcbutton, infoBox, PrintOutButton, ShipLabel, ShipInput, InfoBoxExplain, ShipGenerateButton);

        Application.Top.Add(window);

        Application.Run();
        Application.Shutdown();
    }
}