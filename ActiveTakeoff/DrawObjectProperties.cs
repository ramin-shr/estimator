using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using DevComponents.DotNetBar;

namespace QuoterPlan
{
	public class DrawObjectProperties : Component
	{
		[Browsable(true)]
		[Category("Properties")]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = ((value.Length >= 50) ? value.Substring(0, 50) : value);
			}
		}

		[Browsable(true)]
		[Category("Properties")]
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = ((value.Length >= 5) ? value.Substring(0, 5) : value);
			}
		}

		[Browsable(true)]
		[Category("Properties")]
		public Color Color
		{
			get
			{
				return this.color;
			}
			set
			{
				this.color = value;
			}
		}

		[Browsable(true)]
		[Category("Properties")]
		public Color FillColor
		{
			get
			{
				return this.fillColor;
			}
			set
			{
				this.fillColor = value;
			}
		}

		[Browsable(true)]
		[Category("Properties")]
		[Editor(typeof(UITypeEditorHatchStyle), typeof(UITypeEditor))]
		public HatchStylePickerCombo.HatchStylePickerEnum Pattern
		{
			get
			{
				return this.pattern;
			}
			set
			{
				this.pattern = value;
			}
		}

		[PropertySliderEditor(1, 32, true, 20)]
		[Browsable(true)]
		[Category("Properties")]
		public int PenWidth
		{
			get
			{
				return this.penWidth;
			}
			set
			{
				this.penWidth = value;
			}
		}

		[PropertySliderEditor(8, 96, true, 20)]
		[Browsable(true)]
		[Category("Properties")]
		public int FontSize
		{
			get
			{
				return this.fontSize;
			}
			set
			{
				this.fontSize = value;
			}
		}

		[Browsable(true)]
		[Category("Properties")]
		public DrawCounter.CounterShapeTypeEnum Shape
		{
			get
			{
				return this.shape;
			}
			set
			{
				this.shape = value;
			}
		}

		[PropertySliderEditor(10, 150, true, 25)]
		[Browsable(true)]
		[Category("Properties")]
		public int CounterSize
		{
			get
			{
				return this.counterSize;
			}
			set
			{
				this.counterSize = value;
			}
		}

		[PropertySliderEditor(1, 96, true, 20)]
		[Browsable(true)]
		[Category("Properties")]
		public int MaxRows
		{
			get
			{
				return this.maxRow;
			}
			set
			{
				this.maxRow = value;
			}
		}

		[Browsable(true)]
		[Category("Properties")]
		public object SlopeFactor
		{
			get
			{
				return this.slopeFactor;
			}
			set
			{
				this.slopeFactor = value;
			}
		}

		[Browsable(true)]
		[Category("Properties")]
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string Comment
		{
			get
			{
				return this.comment;
			}
			set
			{
				this.comment = value;
			}
		}

		[Browsable(true)]
		[Category("Properties")]
		public bool ShowMeasure
		{
			get
			{
				return this.showMeasure;
			}
			set
			{
				this.showMeasure = value;
			}
		}

		[Browsable(true)]
		[Category("Properties")]
		public bool Visible
		{
			get
			{
				return this.visible;
			}
			set
			{
				this.visible = value;
			}
		}

		[Browsable(true)]
		[Category("SelectedObjectsProperties")]
		public string Label
		{
			get
			{
				return this.label;
			}
			set
			{
				this.label = ((value.Length >= 50) ? value.Substring(0, 50) : value);
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public object PresetChoice1
		{
			get
			{
				return this.presetChoice1;
			}
			set
			{
				this.presetChoice1 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public object PresetChoice2
		{
			get
			{
				return this.presetChoice2;
			}
			set
			{
				this.presetChoice2 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public object PresetChoice3
		{
			get
			{
				return this.presetChoice3;
			}
			set
			{
				this.presetChoice3 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public object PresetChoice4
		{
			get
			{
				return this.presetChoice4;
			}
			set
			{
				this.presetChoice4 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public object PresetChoice5
		{
			get
			{
				return this.presetChoice5;
			}
			set
			{
				this.presetChoice5 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public object PresetChoice6
		{
			get
			{
				return this.presetChoice6;
			}
			set
			{
				this.presetChoice6 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public object PresetChoice7
		{
			get
			{
				return this.presetChoice7;
			}
			set
			{
				this.presetChoice7 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public object PresetChoice8
		{
			get
			{
				return this.presetChoice8;
			}
			set
			{
				this.presetChoice8 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public object PresetChoice9
		{
			get
			{
				return this.presetChoice9;
			}
			set
			{
				this.presetChoice9 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public object PresetChoice10
		{
			get
			{
				return this.presetChoice10;
			}
			set
			{
				this.presetChoice10 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public object PresetChoice11
		{
			get
			{
				return this.presetChoice11;
			}
			set
			{
				this.presetChoice11 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public object PresetChoice12
		{
			get
			{
				return this.presetChoice12;
			}
			set
			{
				this.presetChoice12 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public object PresetChoice13
		{
			get
			{
				return this.presetChoice13;
			}
			set
			{
				this.presetChoice13 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public object PresetChoice14
		{
			get
			{
				return this.presetChoice14;
			}
			set
			{
				this.presetChoice14 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public object PresetChoice15
		{
			get
			{
				return this.presetChoice15;
			}
			set
			{
				this.presetChoice15 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public object PresetChoice16
		{
			get
			{
				return this.presetChoice16;
			}
			set
			{
				this.presetChoice16 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public object PresetChoice17
		{
			get
			{
				return this.presetChoice17;
			}
			set
			{
				this.presetChoice17 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public object PresetChoice18
		{
			get
			{
				return this.presetChoice18;
			}
			set
			{
				this.presetChoice18 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public object PresetChoice19
		{
			get
			{
				return this.presetChoice19;
			}
			set
			{
				this.presetChoice19 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public object PresetChoice20
		{
			get
			{
				return this.presetChoice20;
			}
			set
			{
				this.presetChoice20 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public object PresetChoice21
		{
			get
			{
				return this.presetChoice21;
			}
			set
			{
				this.presetChoice21 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public object PresetChoice22
		{
			get
			{
				return this.presetChoice22;
			}
			set
			{
				this.presetChoice22 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public object PresetChoice23
		{
			get
			{
				return this.presetChoice23;
			}
			set
			{
				this.presetChoice23 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public object PresetChoice24
		{
			get
			{
				return this.presetChoice24;
			}
			set
			{
				this.presetChoice24 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public object PresetChoice25
		{
			get
			{
				return this.presetChoice25;
			}
			set
			{
				this.presetChoice25 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public string PresetField1
		{
			get
			{
				return this.presetField1;
			}
			set
			{
				this.presetField1 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public string PresetField2
		{
			get
			{
				return this.presetField2;
			}
			set
			{
				this.presetField2 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public string PresetField3
		{
			get
			{
				return this.presetField3;
			}
			set
			{
				this.presetField3 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public string PresetField4
		{
			get
			{
				return this.presetField4;
			}
			set
			{
				this.presetField4 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public string PresetField5
		{
			get
			{
				return this.presetField5;
			}
			set
			{
				this.presetField5 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public string PresetField6
		{
			get
			{
				return this.presetField6;
			}
			set
			{
				this.presetField6 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public string PresetField7
		{
			get
			{
				return this.presetField7;
			}
			set
			{
				this.presetField7 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public string PresetField8
		{
			get
			{
				return this.presetField8;
			}
			set
			{
				this.presetField8 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public string PresetField9
		{
			get
			{
				return this.presetField9;
			}
			set
			{
				this.presetField9 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public string PresetField10
		{
			get
			{
				return this.presetField10;
			}
			set
			{
				this.presetField10 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public string PresetField11
		{
			get
			{
				return this.presetField11;
			}
			set
			{
				this.presetField11 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public string PresetField12
		{
			get
			{
				return this.presetField12;
			}
			set
			{
				this.presetField12 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public string PresetField13
		{
			get
			{
				return this.presetField13;
			}
			set
			{
				this.presetField13 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public string PresetField14
		{
			get
			{
				return this.presetField14;
			}
			set
			{
				this.presetField14 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public string PresetField15
		{
			get
			{
				return this.presetField15;
			}
			set
			{
				this.presetField15 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public string PresetField16
		{
			get
			{
				return this.presetField16;
			}
			set
			{
				this.presetField16 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public string PresetField17
		{
			get
			{
				return this.presetField17;
			}
			set
			{
				this.presetField17 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public string PresetField18
		{
			get
			{
				return this.presetField18;
			}
			set
			{
				this.presetField18 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public string PresetField19
		{
			get
			{
				return this.presetField19;
			}
			set
			{
				this.presetField19 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public string PresetField20
		{
			get
			{
				return this.presetField20;
			}
			set
			{
				this.presetField20 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public string PresetField21
		{
			get
			{
				return this.presetField21;
			}
			set
			{
				this.presetField21 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public string PresetField22
		{
			get
			{
				return this.presetField22;
			}
			set
			{
				this.presetField22 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public string PresetField23
		{
			get
			{
				return this.presetField23;
			}
			set
			{
				this.presetField23 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public string PresetField24
		{
			get
			{
				return this.presetField24;
			}
			set
			{
				this.presetField24 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetProperties")]
		public string PresetField25
		{
			get
			{
				return this.presetField25;
			}
			set
			{
				this.presetField25 = value;
			}
		}

		[PropertySliderEditor(0, 90, true, 35)]
		[Browsable(true)]
		[Category("PresetCustomRendering")]
		public int RenderingAngle
		{
			get
			{
				return this.renderingAngle;
			}
			set
			{
				this.renderingAngle = value;
			}
		}

		[PropertySliderEditor(-100, 100, true, 35)]
		[Browsable(true)]
		[Category("PresetCustomRendering")]
		public int RenderingOffsetX
		{
			get
			{
				return this.renderingOffsetX;
			}
			set
			{
				this.renderingOffsetX = value;
			}
		}

		[PropertySliderEditor(-100, 100, true, 35)]
		[Browsable(true)]
		[Category("PresetCustomRendering")]
		public int RenderingOffsetY
		{
			get
			{
				return this.renderingOffsetY;
			}
			set
			{
				this.renderingOffsetY = value;
			}
		}

		[Browsable(true)]
		[Category("Values")]
		[ReadOnly(true)]
		public string GroupCount
		{
			get
			{
				return this.groupCount;
			}
			set
			{
				this.groupCount = value;
			}
		}

		[Browsable(true)]
		[Category("Values")]
		[ReadOnly(true)]
		public string Length
		{
			get
			{
				return this.length;
			}
			set
			{
				this.length = value;
			}
		}

		[Browsable(true)]
		[Category("Values")]
		[ReadOnly(true)]
		public string Area
		{
			get
			{
				return this.area;
			}
			set
			{
				this.area = value;
			}
		}

		[Browsable(true)]
		[Category("Values")]
		[ReadOnly(true)]
		public string Deduction
		{
			get
			{
				return this.deduction;
			}
			set
			{
				this.deduction = value;
			}
		}

		[Browsable(true)]
		[Category("Values")]
		[ReadOnly(true)]
		public string AreaMinusDeduction
		{
			get
			{
				return this.areaMinusDeduction;
			}
			set
			{
				this.areaMinusDeduction = value;
			}
		}

		[Browsable(true)]
		[Category("Values")]
		[ReadOnly(true)]
		public string Perimeter
		{
			get
			{
				return this.perimeter;
			}
			set
			{
				this.perimeter = value;
			}
		}

		[Browsable(true)]
		[Category("Values")]
		[ReadOnly(true)]
		public string PerimeterPlusDeduction
		{
			get
			{
				return this.perimeterPlusDeduction;
			}
			set
			{
				this.perimeterPlusDeduction = value;
			}
		}

		[Browsable(true)]
		[Category("Values")]
		[ReadOnly(true)]
		public string Openings
		{
			get
			{
				return this.openings;
			}
			set
			{
				this.openings = value;
			}
		}

		[Browsable(true)]
		[Category("Values")]
		[ReadOnly(true)]
		public string PerimeterMinusOpening
		{
			get
			{
				return this.perimeterMinusOpening;
			}
			set
			{
				this.perimeterMinusOpening = value;
			}
		}

		[Browsable(true)]
		[Category("Values")]
		[ReadOnly(true)]
		public string DropLength
		{
			get
			{
				return this.dropLength;
			}
			set
			{
				this.dropLength = value;
			}
		}

		[Browsable(true)]
		[Category("Values")]
		[ReadOnly(true)]
		public string NetLength
		{
			get
			{
				return this.netLength;
			}
			set
			{
				this.netLength = value;
			}
		}

		[Browsable(true)]
		[Category("Values")]
		[ReadOnly(true)]
		public string DropsCount
		{
			get
			{
				return this.dropsCount;
			}
			set
			{
				this.dropsCount = value;
			}
		}

		[Browsable(true)]
		[Category("Values")]
		[ReadOnly(true)]
		public string DeductionsCount
		{
			get
			{
				return this.deductionsCount;
			}
			set
			{
				this.deductionsCount = value;
			}
		}

		[Browsable(true)]
		[Category("Values")]
		[ReadOnly(true)]
		public string OpeningsCount
		{
			get
			{
				return this.openingsCount;
			}
			set
			{
				this.openingsCount = value;
			}
		}

		[Browsable(true)]
		[Category("Values")]
		[ReadOnly(true)]
		public string CornersCount
		{
			get
			{
				return this.cornersCount;
			}
			set
			{
				this.cornersCount = value;
			}
		}

		[Browsable(true)]
		[Category("Values")]
		[ReadOnly(true)]
		public string EndsCount
		{
			get
			{
				return this.endsCount;
			}
			set
			{
				this.endsCount = value;
			}
		}

		[Browsable(true)]
		[Category("Values")]
		[ReadOnly(true)]
		public string SegmentsCount
		{
			get
			{
				return this.segmentsCount;
			}
			set
			{
				this.segmentsCount = value;
			}
		}

		[Browsable(true)]
		[Category("Values")]
		[ReadOnly(true)]
		public string TeesCount
		{
			get
			{
				return this.teesCount;
			}
			set
			{
				this.teesCount = value;
			}
		}

		[Browsable(true)]
		[Category("Values")]
		[ReadOnly(true)]
		public string Angle
		{
			get
			{
				return this.angle;
			}
			set
			{
				this.angle = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult1
		{
			get
			{
				return this.presetResult1;
			}
			set
			{
				this.presetResult1 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult2
		{
			get
			{
				return this.presetResult2;
			}
			set
			{
				this.presetResult2 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult3
		{
			get
			{
				return this.presetResult3;
			}
			set
			{
				this.presetResult3 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult4
		{
			get
			{
				return this.presetResult4;
			}
			set
			{
				this.presetResult4 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult5
		{
			get
			{
				return this.presetResult5;
			}
			set
			{
				this.presetResult5 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult6
		{
			get
			{
				return this.presetResult6;
			}
			set
			{
				this.presetResult6 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult7
		{
			get
			{
				return this.presetResult7;
			}
			set
			{
				this.presetResult7 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult8
		{
			get
			{
				return this.presetResult8;
			}
			set
			{
				this.presetResult8 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult9
		{
			get
			{
				return this.presetResult9;
			}
			set
			{
				this.presetResult9 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult10
		{
			get
			{
				return this.presetResult10;
			}
			set
			{
				this.presetResult10 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult11
		{
			get
			{
				return this.presetResult11;
			}
			set
			{
				this.presetResult11 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult12
		{
			get
			{
				return this.presetResult12;
			}
			set
			{
				this.presetResult12 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult13
		{
			get
			{
				return this.presetResult13;
			}
			set
			{
				this.presetResult13 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult14
		{
			get
			{
				return this.presetResult14;
			}
			set
			{
				this.presetResult14 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult15
		{
			get
			{
				return this.presetResult15;
			}
			set
			{
				this.presetResult15 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult16
		{
			get
			{
				return this.presetResult16;
			}
			set
			{
				this.presetResult16 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult17
		{
			get
			{
				return this.presetResult17;
			}
			set
			{
				this.presetResult17 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult18
		{
			get
			{
				return this.presetResult18;
			}
			set
			{
				this.presetResult18 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult19
		{
			get
			{
				return this.presetResult19;
			}
			set
			{
				this.presetResult19 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult20
		{
			get
			{
				return this.presetResult20;
			}
			set
			{
				this.presetResult20 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult21
		{
			get
			{
				return this.presetResult21;
			}
			set
			{
				this.presetResult21 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult22
		{
			get
			{
				return this.presetResult22;
			}
			set
			{
				this.presetResult22 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult23
		{
			get
			{
				return this.presetResult23;
			}
			set
			{
				this.presetResult23 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult24
		{
			get
			{
				return this.presetResult24;
			}
			set
			{
				this.presetResult24 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult25
		{
			get
			{
				return this.presetResult25;
			}
			set
			{
				this.presetResult25 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult26
		{
			get
			{
				return this.presetResult26;
			}
			set
			{
				this.presetResult26 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult27
		{
			get
			{
				return this.presetResult27;
			}
			set
			{
				this.presetResult27 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult28
		{
			get
			{
				return this.presetResult28;
			}
			set
			{
				this.presetResult28 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult29
		{
			get
			{
				return this.presetResult29;
			}
			set
			{
				this.presetResult29 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult30
		{
			get
			{
				return this.presetResult30;
			}
			set
			{
				this.presetResult30 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult31
		{
			get
			{
				return this.presetResult31;
			}
			set
			{
				this.presetResult31 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult32
		{
			get
			{
				return this.presetResult32;
			}
			set
			{
				this.presetResult32 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult33
		{
			get
			{
				return this.presetResult33;
			}
			set
			{
				this.presetResult33 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult34
		{
			get
			{
				return this.presetResult34;
			}
			set
			{
				this.presetResult34 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult35
		{
			get
			{
				return this.presetResult35;
			}
			set
			{
				this.presetResult35 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult36
		{
			get
			{
				return this.presetResult36;
			}
			set
			{
				this.presetResult36 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult37
		{
			get
			{
				return this.presetResult37;
			}
			set
			{
				this.presetResult37 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult38
		{
			get
			{
				return this.presetResult38;
			}
			set
			{
				this.presetResult38 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult39
		{
			get
			{
				return this.presetResult39;
			}
			set
			{
				this.presetResult39 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult40
		{
			get
			{
				return this.presetResult40;
			}
			set
			{
				this.presetResult40 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult41
		{
			get
			{
				return this.presetResult41;
			}
			set
			{
				this.presetResult41 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult42
		{
			get
			{
				return this.presetResult42;
			}
			set
			{
				this.presetResult42 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult43
		{
			get
			{
				return this.presetResult43;
			}
			set
			{
				this.presetResult43 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult44
		{
			get
			{
				return this.presetResult44;
			}
			set
			{
				this.presetResult44 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult45
		{
			get
			{
				return this.presetResult45;
			}
			set
			{
				this.presetResult45 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult46
		{
			get
			{
				return this.presetResult46;
			}
			set
			{
				this.presetResult46 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult47
		{
			get
			{
				return this.presetResult47;
			}
			set
			{
				this.presetResult47 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult48
		{
			get
			{
				return this.presetResult48;
			}
			set
			{
				this.presetResult48 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult49
		{
			get
			{
				return this.presetResult49;
			}
			set
			{
				this.presetResult49 = value;
			}
		}

		[Browsable(true)]
		[Category("PresetResults")]
		[ReadOnly(true)]
		public string PresetResult50
		{
			get
			{
				return this.presetResult50;
			}
			set
			{
				this.presetResult50 = value;
			}
		}

		public DrawObjectProperties()
		{
		}

		public const int maxPresetChoices = 25;

		public const int maxPresetFields = 25;

		public const int maxPresetVisualRendering = 3;

		public const int maxPresetResults = 50;

		protected string name;

		protected string text;

		protected Color color;

		protected Color fillColor;

		protected HatchStylePickerCombo.HatchStylePickerEnum pattern;

		protected int penWidth;

		protected int fontSize;

		protected DrawCounter.CounterShapeTypeEnum shape;

		protected int counterSize;

		protected int maxRow;

		protected object slopeFactor;

		protected string comment;

		protected bool showMeasure;

		protected bool visible;

		protected string label;

		protected object presetChoice1;

		protected object presetChoice2;

		protected object presetChoice3;

		protected object presetChoice4;

		protected object presetChoice5;

		protected object presetChoice6;

		protected object presetChoice7;

		protected object presetChoice8;

		protected object presetChoice9;

		protected object presetChoice10;

		protected object presetChoice11;

		protected object presetChoice12;

		protected object presetChoice13;

		protected object presetChoice14;

		protected object presetChoice15;

		protected object presetChoice16;

		protected object presetChoice17;

		protected object presetChoice18;

		protected object presetChoice19;

		protected object presetChoice20;

		protected object presetChoice21;

		protected object presetChoice22;

		protected object presetChoice23;

		protected object presetChoice24;

		protected object presetChoice25;

		protected string presetField1;

		protected string presetField2;

		protected string presetField3;

		protected string presetField4;

		protected string presetField5;

		protected string presetField6;

		protected string presetField7;

		protected string presetField8;

		protected string presetField9;

		protected string presetField10;

		protected string presetField11;

		protected string presetField12;

		protected string presetField13;

		protected string presetField14;

		protected string presetField15;

		protected string presetField16;

		protected string presetField17;

		protected string presetField18;

		protected string presetField19;

		protected string presetField20;

		protected string presetField21;

		protected string presetField22;

		protected string presetField23;

		protected string presetField24;

		protected string presetField25;

		protected int renderingAngle;

		protected int renderingOffsetX;

		protected int renderingOffsetY;

		protected string groupCount;

		protected string length;

		protected string area;

		protected string deduction;

		protected string areaMinusDeduction;

		protected string perimeter;

		protected string perimeterPlusDeduction;

		protected string openings;

		protected string perimeterMinusOpening;

		protected string dropLength;

		protected string netLength;

		protected string dropsCount;

		protected string deductionsCount;

		protected string openingsCount;

		protected string cornersCount;

		protected string endsCount;

		protected string segmentsCount;

		protected string teesCount;

		protected string angle;

		protected string presetResult1;

		protected string presetResult2;

		protected string presetResult3;

		protected string presetResult4;

		protected string presetResult5;

		protected string presetResult6;

		protected string presetResult7;

		protected string presetResult8;

		protected string presetResult9;

		protected string presetResult10;

		protected string presetResult11;

		protected string presetResult12;

		protected string presetResult13;

		protected string presetResult14;

		protected string presetResult15;

		protected string presetResult16;

		protected string presetResult17;

		protected string presetResult18;

		protected string presetResult19;

		protected string presetResult20;

		protected string presetResult21;

		protected string presetResult22;

		protected string presetResult23;

		protected string presetResult24;

		protected string presetResult25;

		protected string presetResult26;

		protected string presetResult27;

		protected string presetResult28;

		protected string presetResult29;

		protected string presetResult30;

		protected string presetResult31;

		protected string presetResult32;

		protected string presetResult33;

		protected string presetResult34;

		protected string presetResult35;

		protected string presetResult36;

		protected string presetResult37;

		protected string presetResult38;

		protected string presetResult39;

		protected string presetResult40;

		protected string presetResult41;

		protected string presetResult42;

		protected string presetResult43;

		protected string presetResult44;

		protected string presetResult45;

		protected string presetResult46;

		protected string presetResult47;

		protected string presetResult48;

		protected string presetResult49;

		protected string presetResult50;
	}
}
