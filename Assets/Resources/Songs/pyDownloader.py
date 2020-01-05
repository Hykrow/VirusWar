from __future__ import unicode_literals
import youtube_dl
import sys

ydl_opts = {
    'format': 'bestaudio/best',
    'postprocessors': [{
        'key': 'FFmpegExtractAudio',
        'preferredcodec': 'mp3',
        'preferredquality': '192',
    }],
    'outtmpl': sys.argv[1] +'/%(title)s-%(id)s.%(ext)s'

}
print(sys.argv[1])
with youtube_dl.YoutubeDL(ydl_opts) as ydl:
    ydl.download([sys.argv[2]])