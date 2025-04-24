import $ from 'jquery';
import React, { useEffect } from 'react';
import { Navigate, Route, Routes } from 'react-router-dom';

import Home from './Page/Home.js';

export const RouteList = [
    { name: 'home', path: '/', component: Home }
]


export default function Router() {

return (
    <div className="shadow p-3" style={{ backgroundColor: '#15202b', minHeight: '100vh' }}>
    <Routes>
      {RouteList.map((route) => (
        route.path !== '' ?
          <Route path={route.path}
            element={<route.component routeProps={{ name: route.name, path: route.path }} />}
            key={route.path} /> : null
      ))}
      <Route path="/" element={<Navigate to={process.env.PUBLIC_URL} />} />
      {/* <Route path="*" element={<NotFound />} /> */}
    </Routes>
  </div>
)
}