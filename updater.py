import os
import shutil
import urllib.request
os.system("TASKKILL /F /IM NScrapper.exe")
try:
  os.remove("NScrapper.exe")
except:
  print("Nu ai NScrapper.exe in PC, se descarcă oricum.")
urllib.request.urlretrieve("https://github.com/Far0/Clip-scrapper/releases/latest/download/Scrapper.exe", "NScrapper.exe")
os.startfile("NScrapper.exe")
username = os.getlogin()
try:
  shutil.rmtree("C:/Users/" +username +"/AppData/Local/Scrapper")
except:
  print("[INFO] Nu ai config file.")