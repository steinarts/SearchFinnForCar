import re
from statistics import  mean, median

class KeyContent:
    def __init__(self, values):
        self.skip = False
        self.askingprice = 0
        #self.year = int(re.findall(r"\d+", values.contents[0].text)[0].replace(" ", ""))
        self.year = int(re.findall(r"\d+", values.contents[0].text)[0].strip())

        km = values.contents[1].text.split(' ')[0].strip().replace(u'\xa0', u' ')

        self.km = int(km.replace(u' ', u''))

        try:
            askingprice = values.contents[2].text.split(' ')[0].strip().replace(u'\xa0', u' ')
            self.skip = True if askingprice == 'Solgt' else False
        except Exception as err:
            self.skip = True
            print(f"Unexpected {err=}, {type(err)=}")
            print('Feil ', self.year, self.km, values.contents )    

        
        self.askingprice = int(askingprice.replace(u' ', u''))  if self.skip == False else 0
        self.skip = True if self.askingprice < 25000 else False        


# Create a KeyContent instance and pass the values from find_content_keys


def search_finn_for_car(finnModelNo):
    import requests
    #import re
    from bs4 import BeautifulSoup
    from datetime import datetime

    key_content = []
    count = 0
    done = False
    highestPrice = 0
    lowestPrice = 999999
    askingprice_values = []
    
    while done==False:
        for pagaNo in range(100):

            url = "https://www.finn.no/car/used/search.html?model=" + finnModelNo + "&page=" + str(pagaNo+1) +"&sort=PUBLISHED_DESC"
            response = requests.get(url)
            html = response.text
            soup = BeautifulSoup(html, "html.parser")

            find_content_keys = soup.find_all("div", {"class": "ads__unit__content__keys"})

            for keyContent in find_content_keys:
                key_content.append(KeyContent(keyContent))

            pagination = soup.find("a", {"class": "button button--pill button--has-icon button--icon-right"})
            if pagination == None:
                done=True
                break

    for key in key_content:
        if key.skip == False:
            askingprice_values.append(key.askingprice)

    medianpris = median(askingprice_values)
    snittpris = mean(askingprice_values)
    highestPrice = max(askingprice_values)
    lowestPrice = min(askingprice_values)
    count = len(askingprice_values)

    return highestPrice,lowestPrice,int(snittpris),int(medianpris), count, datetime.now().strftime('%Y-%m-%d %H:%M:%S')



input = search_finn_for_car('1.749.2000264')
print(input)
