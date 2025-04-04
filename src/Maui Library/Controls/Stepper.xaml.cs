using CommunityToolkit.Maui.Behaviors;
using System.Globalization;

namespace DigitalProduction.Maui.Controls;

public partial class Stepper : ContentView
{
	#region Fields
	#endregion

	#region Construction
 
    public Stepper()
    {
		InitializeComponent();
		Increment = 1;
        ValueLabel.Text = "0";
    }

	#endregion

	#region Bindable Properties

	public static readonly BindableProperty IncrementProperty =
		BindableProperty.Create(nameof(Increment), typeof(double), typeof(Stepper), null,
		validateValue: (bindable, newObject) =>
		{
            if (newObject is null || bindable is not Stepper self)
            {
                return true;
            }
			if ((double)newObject != 0)
			{
				return true;
			}
			return false;
		},
		propertyChanged: (bindable, oldObject, newObject) =>
        {
            if (newObject == oldObject || bindable is not Stepper self)
            {
                return;
            }

			if (newObject != null)
			{
				self.RoundValue();
				if (self.BoundValue())
				{
					self.UpdateText();
				}
				self.UpdateButtonEnabled();
			}
        }
	);

	public double Increment
	{
		get => (double)GetValue(IncrementProperty);
		set => SetValue(IncrementProperty, value);
	}

	public static readonly BindableProperty MaximumProperty =
		BindableProperty.Create(nameof(Maximum), typeof(double), typeof(Stepper), null,
		propertyChanged: (bindable, oldObject, newObject) =>
        {
            if (newObject == oldObject || bindable is not Stepper self)
            {
                return;
            }

			if (newObject != null)
			{
				if (self.BoundValue())
				{
					self.UpdateText();
				}
				self.UpdateButtonEnabled();
			}
        }
	);

	public double Maximum
	{
		get => (double)GetValue(MaximumProperty);
		set => SetValue(MaximumProperty, value);
	}

	public static readonly BindableProperty MinimumProperty =
		BindableProperty.Create(nameof(Minimum), typeof(double), typeof(Stepper), null,
		propertyChanged: (bindable, oldObject, newObject) =>
        {
            if (newObject == oldObject || bindable is not Stepper self)
            {
                return;
            }

			if (newObject != null)
			{
				if (self.BoundValue())
				{
					self.UpdateText();
				}
				self.UpdateButtonEnabled();
			}
		}
	);

	public double Minimum
	{
		get => (double)GetValue(MinimumProperty);
		set => SetValue(MinimumProperty, value);
	}

	public static readonly BindableProperty ValueProperty =
		BindableProperty.Create(nameof(Value), typeof(double), typeof(Stepper), null, BindingMode.TwoWay,
		propertyChanged: (bindable, oldObject, newObject) =>
        {
            if (newObject == oldObject || bindable is not Stepper self)
            {
                return;
            }
			self.UpdateText();
			self.UpdateButtonEnabled();
		}
	);

	public double Value
	{
		get => (double)GetValue(ValueProperty);
		set => SetValue(ValueProperty, value);
	}

	public static readonly BindableProperty MinusButtonStyleProperty =
		BindableProperty.Create(nameof(MinusButtonStyle), typeof(Style), typeof(Stepper), null,
		propertyChanged: (bindable, oldObject, newObject) =>
        {
            if (newObject == oldObject || newObject is not Style newStyle || bindable is not Stepper self)
            {
                return;
            }
			self.MinusButton.Style = newStyle;
		}
	);

	public Style MinusButtonStyle
	{
		get => (Style)GetValue(MinusButtonStyleProperty);
		set => SetValue(MinusButtonStyleProperty, value);
	}

	public static readonly BindableProperty PlusButtonStyleProperty =
		BindableProperty.Create(nameof(PlusButtonStyle), typeof(Style), typeof(Stepper), null,
		propertyChanged: (bindable, oldObject, newObject) =>
        {
            if (newObject == oldObject || newObject is not Style newStyle || bindable is not Stepper self)
            {
                return;
            }
			self.PlusButton.Style = newStyle;
		}
	);

	public Style PlusButtonStyle
	{
		get => (Style)GetValue(PlusButtonStyleProperty);
		set => SetValue(PlusButtonStyleProperty, value);
	}

	public static readonly BindableProperty LabelStyleProperty =
		BindableProperty.Create(nameof(LabelStyle), typeof(Style), typeof(Stepper), null,
		propertyChanged: (bindable, oldObject, newObject) =>
        {
            if (newObject == oldObject || newObject is not Style newStyle || bindable is not Stepper self)
            {
                return;
            }
			self.ValueLabel.Style = newStyle;
		}
	);

	public Style LabelStyle
	{
		get => (Style)GetValue(LabelStyleProperty);
		set => SetValue(LabelStyleProperty, value);
	}

	#endregion

	#region Events
 
    private void OnMinusButtonClicked(object sender, EventArgs eventArgs)
    {
        Value -= Increment;
		BoundValue();
		UpdateText();
		UpdateButtonEnabled();
    }
 
    private void OnPlusButtonClicked(object sender, EventArgs eventArgs)
    {
        Value += Increment;
		BoundValue();
		UpdateText();
		UpdateButtonEnabled();
    }

	#endregion

	#region Methods

	private void RoundValue()
	{
		Value = Math.Round(Value / Increment) * Increment;
		UpdateText();
	}

	private bool BoundValue()
	{
		if (Value < Minimum)
		{
			Value = Minimum;
			return true;
		}
		else
		{
			if (Value > Maximum)
			{
				Value = Maximum;
				return true;
			}
		}

		return false;
	}

	private void UpdateText()
	{
		int decimalPlaces	= BitConverter.GetBytes(decimal.GetBits((decimal)Increment)[3])[2];
		ValueLabel.Text		= Value.ToString("F"+decimalPlaces, CultureInfo.CurrentCulture);
	}

	private void UpdateButtonEnabled()
	{
		if (Value < Minimum + Increment )
		{
			MinusButton.IsEnabled = false;
		}
		else
		{
			if (Value > Minimum)
			{
				MinusButton.IsEnabled = true;
			}
		}

		if (Value > Maximum - Increment)
		{
			PlusButton.IsEnabled = false;
		}
		else
		{
			if (Value < Maximum)
			{
				PlusButton.IsEnabled = true;
			}
		}
	}

	#endregion
}