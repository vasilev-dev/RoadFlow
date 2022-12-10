import React, { useEffect, useState } from 'react';
import API from '../lib/api/AxiosFactory';

function HomePage() {
  const [weather, setWeather] = useState<string | undefined>();
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    setIsLoading(true);
    API.get('/weather').then((response) => {
      setWeather(JSON.stringify(response.data));
      setIsLoading(false);
    });
  }, []);

  return (
    <div>
      {isLoading && 'loading...'}
      {weather && <p>{weather}</p>}
    </div>
  );
}

export default HomePage;
