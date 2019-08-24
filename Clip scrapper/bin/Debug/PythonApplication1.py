import requests
import sys
import os
import dpath.util
from datetime import datetime
import xlwt

# imports

timp = datetime.now()
timp = timp.strftime("%d-%m-%Y_%H-%M-%S")

# timp

headers = {
    "Accept": "application/vnd.twitchtv.v5+json",
    "Client-ID": "cx2zppacbubmk2h9rolhr7gmdgwgoz",
}
# Header pentru browser

os.mkdir(timp)
f = open(timp+"/"+"Slug_"+ timp+".txt","w+")

ex_book = xlwt.Workbook(encoding="utf-8")
ex_sheet = ex_book.add_sheet(timp)
ex_book.save(timp+"/"+"Excel_"+ timp+".xls")
ex_sheet.write(0,0,"Location")
ex_sheet.write(0,1,"Streamer")
ex_sheet.write(0,2,"Views")
ex_sheet.write(0,3,"Duration")
ex_sheet.write(0,4,"Vod")
ex_sheet.write(0,5,"SLUG")

# Folder + excel stuff

etemp = 1
limita = int(5)

stream_file = open("streamer_list.txt", "r")
streamer = stream_file.read().split(',')
for x in range(len(streamer)):
    url = 'https://api.twitch.tv/kraken/clips/top?channel='+ streamer[x] +'&period=month&limit=' + str(limita)
    dictionar = requests.get(url, headers = headers).json()
    temporar=0

    for temporar in range(limita):
        slug = dpath.util.get(dictionar, '/clips/' + str(temporar) + '/slug')
        views = dpath.util.get(dictionar, '/clips/' + str(temporar) + '/views')
        duration = dpath.util.get(dictionar, '/clips/' + str(temporar) + '/duration')
        try:
         vod = dpath.util.get(dictionar, '/clips/' + str(temporar) + '/vod/url')
        except:
            vod = "Vod is unavailable."
        ex_sheet.write(etemp,1,streamer[x])
        ex_sheet.write(etemp,2,views)
        ex_sheet.write(etemp,3,duration)
        ex_sheet.write(etemp,4,vod)
        ex_sheet.write(etemp,5,slug)
        f.write(slug+"\n")
        etemp= etemp+1

ex_book.save(timp+"/"+"Excel_"+ timp+".xls")
f.close