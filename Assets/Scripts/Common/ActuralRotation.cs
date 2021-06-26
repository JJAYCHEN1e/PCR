using UnityEngine;
using System.Collections;

namespace Mechatronics
{
    public class ActuralRotation : MonoBehaviour
    {
        Vector3 _getV3Y;
        Vector3 _setV3Y;
        float _fY;
        private float numY = 0;
        public float rotationY
        {
            get
            {
                _getV3Y = transform.eulerAngles;
                _getV3Y.y += -numY * 360;
                return _getV3Y.y;
            }
            set
            {
                _setV3Y = transform.eulerAngles;
                _fY = value;
                if (_fY < 0)
                {
                    numY = 0;
                    while (_fY < 0)
                    {
                        _fY += 360;
                        numY += 1;
                    }
                }
                else if (_fY >= 360)
                {
                    numY = 0;
                    while (_fY >= 360)
                    {
                        _fY -= 360;
                        numY -= 1;
                    }
                }
                else
                {
                    numY = 0;
                }
                _setV3Y.y = _fY;
                transform.eulerAngles = _setV3Y;
            }
        }
        Vector3 _postSetV3Y;
        public float postRotationY
        {
            get
            {
                return transform.eulerAngles.y;
            }
            set
            {
                _postSetV3Y = transform.eulerAngles;
                _postSetV3Y.y = value;
                transform.eulerAngles = _postSetV3Y;
                numY = 0;
            }
        }
        Vector3 _getV3X;
        Vector3 _setV3X;
        float _fX;
        private float numX = 0;
        public float rotationX
        {
            get
            {
                _getV3X = transform.eulerAngles;
                _getV3X.x += -numX * 360;
                return _getV3X.x;
            }
            set
            {
                _setV3X = transform.eulerAngles;
                _fX = value;
                if (_fX < 0)
                {
                    numX = 0;
                    while (_fX < 0)
                    {
                        _fX += 360;
                        numX += 1;
                    }
                }
                else if (_fX >= 360)
                {
                    numX = 0;
                    while (_fX >= 360)
                    {
                        _fX -= 360;
                        numX -= 1;
                    }
                }
                else
                {
                    numX = 0;
                }
                _setV3X.x = _fX;
                transform.eulerAngles = _setV3X;
            }
        }
        Vector3 _getV3Z;
        Vector3 _setV3Z;
        float _fZ;
        private float numZ = 0;
        public float rotationZ
        {
            get
            {
                _getV3Z = transform.eulerAngles;
                _getV3Z.z += -numZ * 360;
                return _getV3Z.z;
            }
            set
            {
                _setV3Z = transform.eulerAngles;
                _fZ = value;
                if (_fZ < 0)
                {
                    numZ = 0;
                    while (_fZ < 0)
                    {
                        _fZ += 360;
                        numZ += 1;
                    }
                }
                else if (_fZ >= 360)
                {
                    numZ = 0;
                    while (_fZ >= 360)
                    {
                        _fZ -= 360;
                        numZ -= 1;
                    }
                }
                else
                {
                    numZ = 0;
                }
                _setV3Z.z = _fZ;
                transform.eulerAngles = _setV3Z;
            }
        }
        Vector3 _rotationGetV3;
        Vector3 _rotationSetV3;
        public Vector3 rotation
        {
            get
            {
                _rotationGetV3 = transform.eulerAngles;
                _rotationGetV3.x = rotationX;
                _rotationGetV3.y = rotationY;
                _rotationGetV3.z = rotationZ;
                return _rotationGetV3;
            }
            set
            {
                _rotationSetV3 = value;
                rotationX = _rotationSetV3.x;
                rotationY = _rotationSetV3.y;
                rotationZ = _rotationSetV3.z;
            }
        }
    }

}
