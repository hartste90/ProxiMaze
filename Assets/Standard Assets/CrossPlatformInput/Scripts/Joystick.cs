using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityStandardAssets.CrossPlatformInput
{
	public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		public enum AxisOption
		{
			// Options for which axes to use
			Both, // Use both
			OnlyHorizontal, // Only horizontal
			OnlyVertical // Only vertical
		}

		public int MovementRange = 100;
		public AxisOption axesToUse = AxisOption.Both; // The options for the axes that the still will use
		public string horizontalAxisName = "Horizontal"; // The name given to the horizontal axis for the cross platform input
		public string verticalAxisName = "Vertical"; // The name given to the vertical axis for the cross platform input


		public Image touchSpot;
		public Image joystickRange;
		public Image joystickPlaceholder;

		Vector2 touchStartPos;
		Vector3 m_StartPos;
		bool m_UseX; // Toggle for using the x axis
		bool m_UseY; // Toggle for using the Y axis
		CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis; // Reference to the joystick in the cross platform input
		CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis; // Reference to the joystick in the cross platform input

        void Start()
        {
            m_StartPos = transform.position;
			ShowJoystick(false);
			CreateVirtualAxes();

		}

		public void ShowJoystick(bool shouldShow)
        {
			joystickRange.gameObject.SetActive(shouldShow);
			touchSpot.gameObject.SetActive(shouldShow);
			joystickPlaceholder.gameObject.SetActive(!shouldShow);

		}
		void UpdateVirtualAxes(Vector3 value)
		{
			var delta = m_StartPos - value;
			delta.y = -delta.y;
			delta /= MovementRange;
			if (m_UseX)
			{
				m_HorizontalVirtualAxis.Update(-delta.x);
			}

			if (m_UseY)
			{
				m_VerticalVirtualAxis.Update(delta.y);
			}
		}

		void CreateVirtualAxes()
		{
			// set axes to use
			m_UseX = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyHorizontal);
			m_UseY = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyVertical);

			// create new axes based on axes to use
			if (m_UseX)
			{
				m_HorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
				CrossPlatformInputManager.RegisterVirtualAxis(m_HorizontalVirtualAxis);
			}
			if (m_UseY)
			{
				m_VerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
				CrossPlatformInputManager.RegisterVirtualAxis(m_VerticalVirtualAxis);
			}
		}


		public void OnDrag(PointerEventData data)
		{
			
			Vector3 newPos = Vector3.zero;

			if (m_UseX)
			{
				int delta = (int)(data.position.x - m_StartPos.x);
				delta = Mathf.Clamp(delta, - MovementRange, MovementRange);
				newPos.x = delta;
			}

			if (m_UseY)
			{
				int delta = (int)(data.position.y - m_StartPos.y);
				delta = Mathf.Clamp(delta, -MovementRange, MovementRange);
				newPos.y = delta;
			}
			
			transform.position = new Vector3(m_StartPos.x + newPos.x, m_StartPos.y + newPos.y, m_StartPos.z + newPos.z);
			Vector3 maxDistanceJoystickPos = GetDistJoystick(transform.position);
			touchSpot.transform.position = maxDistanceJoystickPos; // transform.position;
			UpdateVirtualAxes(transform.position);
        }

		Vector3 GetDistJoystick(Vector3 rawPos)
        {
			if (Vector3.Distance(rawPos, m_StartPos) >= MovementRange)
			{
				//get vector direction
				Vector3 direction = rawPos - m_StartPos;
				direction.Normalize();

				//mult to max
				return m_StartPos + (direction * MovementRange);
			}
			else
            {
				return rawPos;
            }
        }


		public void OnPointerUp(PointerEventData data)
		{
			ResetJoystick();
			
        }

		public void ResetJoystick()
        {
			transform.position = m_StartPos;
			ShowJoystick(false);
			UpdateVirtualAxes(m_StartPos);
		}


		public void OnPointerDown(PointerEventData data)
		{
			touchStartPos = data.position;
			//m_StartPos = transform.position;
			m_StartPos = data.position;
			ShowJoystick(true);
			joystickRange.transform.position = data.position;
			touchSpot.transform.position = data.position;

		}

		//public static bool IsPointerOverUIElement(PointerEventData data)
		//{
		//	var eventData = data;
		//	var results = new List<RaycastResult>();
		//	EventSystem.current.RaycastAll(eventData, results);
		//	foreach(RaycastResult result in results)
  //          {
		//		if (result.gameObject.activeInHierarchy)
  //              {
		//			Debug.LogFormat("Over ui element: {0}", result.gameObject.name);
		//			return true;
  //              }
  //          }
		//	return false;
		//}


	}
}