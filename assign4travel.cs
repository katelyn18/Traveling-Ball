//Katelyn Jaing
//Email: kjaing18@csu.fullerton.edu
//Assignment 4: Traveling Ball
//CPSC 223N 
//assign4travel.cs

using System;
using System.Windows.Forms;
using System.Drawing;
using System.Timers;

public class TravelBall : Form{

	//labels
	private Label topLabel = new Label();
	private Label speedLabel = new Label();
	private Label angleLabel = new Label();
	private Label coordinateLabel = new Label();

	//buttons
	private Button startPauseButton = new Button();
	private Button exitButton = new Button();
	private bool isStart = true;

	//graph
	Pen solidPen = new Pen( Color.Black );
	Pen dashPen = new Pen( Color.Black );

	//ball
	private static System.Timers.Timer ballTimer = new System.Timers.Timer();
	private double ballCenterX = 0;
	private double ballCenterY = 0;
	private bool ballShown = false;
	private double ballCountX = 0; 
	private double ballCountY = 0;

	//input speed (double)
	TextBox speedBox = new TextBox();
	private double speedNum = 0;
	private bool speed = false;

	//input angle (double)
	TextBox angleBox = new TextBox();
	private double angleNum = 0;
	private bool angle = false;

	//math value
	private double mathX = 0;
	private double mathY = 0;

	//constructor
	public TravelBall(){
		//window background
		Text = "Traveling Ball";
		Size = new Size( 1290, 770 );	//length, width
		BackColor = Color.LightBlue;

		//top label
		topLabel.Text = "Traveling Ball by Katelyn Jaing";
		topLabel.Size = new Size( 1290, 50 );
		topLabel.Location = new Point( 0, 0 );
		topLabel.Font = new Font( "Arial", 14, FontStyle.Regular );
		topLabel.BackColor = Color.LightGreen;
		topLabel.AutoSize = false;
		topLabel.TextAlign = ContentAlignment.MiddleCenter;

		//start/pause button
		startPauseButton.Text = "Start";
		startPauseButton.Size = new Size( 100, 70 );
		startPauseButton.Location = new Point( 990, 650 );
		startPauseButton.BackColor = Color.White;
		startPauseButton.Click += new EventHandler( startPauseButtonClick );
		
		//stop button
		exitButton.Text = "Exit";
		exitButton.Size = new Size( 100, 70 );
		exitButton.Location = new Point( 1100, 650 ); //x, y
		exitButton.BackColor = Color.White;
		exitButton.Click += new EventHandler( exitButtonClick );

		//graph lines
		solidPen.Width = 2.0f;
		dashPen.Width = 2.0f;
		float[] dashPattern = { 4.0f, 1.0f, 2.0f, 1.0f };
		dashPen.DashPattern = dashPattern; 

		//speed label
		speedLabel.Text = "Input C# Speed";
		speedLabel.Size = new Size( 100, 30 );
		speedLabel.Location = new Point( 20, 650 );
		speedLabel.Font = new Font( "Arial", 10, FontStyle.Bold );

		//speed box
		speedBox.Enabled = true;
		speedBox.AcceptsReturn = true;
		speedBox.Location = new Point( 20, 680 );
		speedBox.KeyDown += new KeyEventHandler(  speedReturn );

		//angle label
		angleLabel.Text = "Input Angle Degree";
		angleLabel.Size = new Size( 130, 30 );
		angleLabel.Location = new Point( 170, 650 );
		angleLabel.Font = new Font( "Arial", 10, FontStyle.Bold );

		//angle box
		angleBox.Enabled = true;
		angleBox.AcceptsReturn = true;
		angleBox.Location = new Point( 170, 680 );
		angleBox.KeyDown += new KeyEventHandler( angleReturn );

		//coordinate label
		coordinateLabel.Text = "  Coordinates of Center of Ball\n\nC# system: (" + Math.Round( ballCenterX, 2 ) + ", " + Math.Round( ballCenterY, 2 ) + ")\n\nMath system: (" + Math.Round( mathX, 2 ) + ", " + Math.Round( mathY, 2 ) + ")";
		coordinateLabel.BorderStyle = BorderStyle.FixedSingle;
		coordinateLabel.Size = new Size( 200, 70 );
		coordinateLabel.Location = new Point( 560, 650 );
		coordinateLabel.Font = new Font( "Arial", 10, FontStyle.Bold );

		//add to window
		Controls.Add( topLabel );
		Controls.Add( exitButton );
		Controls.Add( startPauseButton );
		Controls.Add( speedLabel );
		Controls.Add( speedBox );
		Controls.Add( angleLabel );
		Controls.Add( angleBox );
		Controls.Add( coordinateLabel );

		//timer
		ballTimer.Enabled = false;
		ballTimer.Elapsed += new ElapsedEventHandler( ballSwitch );
	}

	//paint ball and graph
	protected override void OnPaint( PaintEventArgs args ){
		Graphics g = args.Graphics;

		//draw the graph
			//draw +y
		g.DrawLine( solidPen, 645.0f, 50.0f, 645.0f, 300.0f );
			//draw +x
		g.DrawLine( solidPen, 645.0f, 300.0f, 1270.0f, 300.0f );
			//draw -y
		g.DrawLine( dashPen, 645.0f, 300.0f, 645.0f, 600.0f );
			//draw -x
		g.DrawLine( dashPen, 10.0f, 300.0f, 645.0f, 300.0f );
		
		//draw the ball
		if( ballShown == true ){
			g.FillEllipse( Brushes.Orange, (float)ballCenterX-15, (float)ballCenterY-15, 30, 30 );
		}
		
	}

	//start button
	protected void startPauseButtonClick( Object sender, EventArgs args ){
		//start ball timer
		if( isStart == true ){
			ballTimer.Enabled = true;
			ballTimer.Interval = 700;

			isStart = false;
			startPauseButton.Text = "Pause";
		}
		else if( isStart == false ){
			ballTimer.Enabled = false;
			isStart = true;
			startPauseButton.Text = "Start";
		}

		//get speed input
		if( speed == true ){
			try{
				speedNum = double.Parse( speedBox.Text );
			}
			catch{
				System.Console.WriteLine( "Invalidate. Input assume 50.0" );
				speedNum = 50.0;
			}		
		}
		else if( speed == false ){
			System.Console.WriteLine( "Invalidate. Input assume 50.0" );
			speedNum = 50.0;
		}

		//get angle input
		if( angle == true ){
			try{
				angleNum = double.Parse( angleBox.Text );
			}
			catch{
				System.Console.WriteLine( "Invalidate. Input assume 45.0" );
				angleNum = 45.0;
			}
		}
		else if( angle == false ){
			System.Console.WriteLine( "Invalidate. Input assume 45.0" );
			angleNum = 45.0;
		}
		Invalidate();
	}

	//exit button
	protected void exitButtonClick( Object sender, EventArgs args ){
		Close();
	}

	//get speed input
	protected void speedReturn( Object sender, KeyEventArgs args ){
		string s = null;
		s = speedBox.Text;
		if( s.Length > 0 ){
			speed = true;
		}
		Invalidate();
	}

	//get angle input
	protected void angleReturn( Object sender, KeyEventArgs args ){
		string a = null;
		a = angleBox.Text;
		if( a.Length > 0 ){	
			angle = true;
		}
		Invalidate();
	}

	//traveling ball and get ball coordinate on mouse click
	protected override void OnMouseDown( MouseEventArgs args ){
		ballCenterX = args.X;
		ballCenterY = args.Y;
		
		ballShown = true;

		mathX =  ( ballCenterX - 650 ) / 35;
		mathY = -( ( ballCenterY - 300 ) / 35 );
		coordinateLabel.Text = "  Coordinates of Center of Ball\n\nC# system: (" + Math.Round( ballCenterX, 2 ) + ", " + Math.Round( ballCenterY, 2 ) + ")\n\nMath system: (" + Math.Round( mathX, 2 ) + ", " + Math.Round( mathY, 2 ) + ")";
		Invalidate();
	}

	//timer for ball and update ball movement
	protected void ballSwitch( Object sender, EventArgs args ){
		double x = 0;
		double y = 0;

		if( ballCountX > 1290 || ballCountX < 0 || ballCountY > 770 || ballCountY < 0 ){
			ballTimer.Enabled = false;
			ballShown = false;
			startPauseButton.Text = "Start";
			isStart = true;
		}

		x = Math.Cos( angleNum * ( Math.PI / 180.0 ) );
		x *= speedNum;

		y = Math.Sin( angleNum * ( Math.PI / 180.0 ) );
		y *= speedNum;

		ballCenterX += x;
		ballCenterY += ( -1 * y );
		ballCountX = ballCenterX;
		ballCountY = ballCenterY;

		mathX = ( ballCenterX - 650 ) / 35;
		mathY = -( ( ballCenterY - 300 ) / 35 );
		coordinateLabel.Text = "  Coordinates of Center of Ball\n\nC# system: (" + Math.Round( ballCenterX, 2 ) + ", " + Math.Round( ballCenterY, 2 ) + ")\n\nMath system: (" + Math.Round( mathX, 2 ) + ", " + Math.Round( mathY, 2 ) + ")";
		Invalidate();
	}
}
