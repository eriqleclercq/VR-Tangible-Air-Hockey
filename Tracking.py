import cv2 as cv
import cvzone
from cvzone.ColorModule import ColorFinder
import socket

print("Trying to open first camera")
capX = cv.VideoCapture(0)
if not capX.isOpened():
    print("Cannot open camera")
    exit()

print("Trying to open second camera")
capZ = cv.VideoCapture(1)
if not capZ.isOpened():
    print("Cannot open second camera")
    exit()


print("Camera 1 & 2 opened succesfully")
#sets the dimensions of the cameras
capX.set(3, 1920)
capX.set(4,1080)
capZ.set(3, 1920)
capZ.set(4,1080)

print("Dimensions of cameras done succesfully")
successX, imgX = capX.read()

h,w,_ = imgX.shape


# might need to change this value depending on where the camera is located
# to do that, set the color finder to True and fine tune the hsv values
myColourFinder = ColorFinder(False)
hsvVals = {'hmin': 142, 'smin': 105, 'vmin': 0, 'hmax': 176, 'smax': 255, 'vmax': 255}

imgColourX, maskX = myColourFinder.update(imgX, hsvVals)
imgContoursOG, contoursOG = cvzone.findContours(imgX, maskX, minArea = 3000)
contoursOG = contoursOG[0]['cnt']

successZ, successZ = capZ.read()

print("Read images successfully")


sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverAddressPort = ("127.0.0.1", 5055)

print("starting camera capture")

while True:
    successX, imgX = capX.read()
    successZ, successZ = capZ.read()

    imgColourX, maskX = myColourFinder.update(imgX, hsvVals)
    imgContoursX, contoursX = cvzone.findContours(imgX, maskX, minArea = 3000)


    imgColourZ, maskZ = myColourFinder.update(successZ, hsvVals)
    imgContoursZ, contoursZ = cvzone.findContours(successZ, maskZ, minArea = 3000)

    if (len(contoursX) != 0) & (len(contoursZ) != 0):
        ret = cv.matchShapes(contoursOG, contoursX[0]['cnt'], 1, 0.0)
        if ret < 0.45:
            x = - (contoursX[0]['center'][0] - 1/2*w) / 1000
            y = - (contoursZ[0]['center'][0] - 1/2*w) / 1000

            data = x,y
            w - contoursX[0]['center'][0],h - contoursX[0]['center'][1] 
            print(data, "Contour match:", ret)
            sock.sendto(str.encode(str(data)), serverAddressPort)
    cv.waitKey(1)