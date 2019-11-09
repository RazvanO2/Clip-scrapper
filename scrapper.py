import requests
import sys
import os
import dpath.util
from datetime import datetime
from openpyxl import Workbook
# imports
headers = {
    "Accept": "application/vnd.twitchtv.v5+json",
    "Client-ID": "cx2zppacbubmk2h9rolhr7gmdgwgoz",
}
timp = datetime.now()
timp = timp.strftime("%Y%m%d%H%M%S")

f = open("timp.txt","w+")
f.write(timp)
os.mkdir(timp)
os.mkdir(timp+"/"+"Clips")
f = open(timp+"/"+"Slug.txt","w+")

workbook = Workbook()
sheet= workbook.active

sheet.title = "Start"
tableheaders = ["Location","Streamer","Views","Duration","VOD","Slug","Title"]
for a in range(len(tableheaders)):
    sheet[chr(ord("A") + a) +"1"] = tableheaders[a]
    
workbook.save(filename=timp+"/"+"Excel.xlsx")
# Folder + excel stuff

etemp = 2
limita = int(input("Clips perStreamer [Default=5]: ")
if limita=="":
    limita=int(5)
stream_file = open("streamer_list.txt", "r")
streamer = stream_file.read().splitlines()
for x in range(len(streamer)):
    url = 'https://api.twitch.tv/kraken/clips/top?channel='+ streamer[x] +'&period=day&limit=20'
    dictionar = requests.get(url, headers = headers).json()
    temporar = 0
    for temporar in range(10):
        try:
            slug = dpath.util.get(dictionar, '/clips/{}/slug'.format(temporar))
            views = dpath.util.get(dictionar, '/clips/{}/views'.format(temporar))
            duration = dpath.util.get(dictionar, '/clips/{}/duration'.format(temporar))
            title = dpath.util.get(dictionar, '/clips/{}/title'.format(temporar))
            try:
                vod = dpath.util.get(dictionar, '/clips/{}/vod/url'.format(temporar))
            except:
                vod = "Vod is unavailable."
        except:
            slug=""
            vod="Invalid"
            duration=0
            views=0   
        sheet["B"+ str(etemp)] = streamer[x]
        sheet["C"+ str(etemp)] = views
        sheet["D"+ str(etemp)] = duration
        sheet["E"+ str(etemp)] = "=HYPERLINK("+'"'+ vod  +'"'+")"
        sheet["F"+ str(etemp)] = slug
        sheet["G"+ str(etemp)] = title
        sheet
        f.write(slug+"\n")
        etemp= etemp+1

workbook.save(filename=timp+"/"+"Excel.xlsx")
f.close
