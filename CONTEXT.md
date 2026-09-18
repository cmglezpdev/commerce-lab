# Commerce

Este contexto describe las ofertas del catálogo que el comercio puede configurar y vender. Distingue una ficha de catálogo de la unidad concreta que el cliente puede comprar.

## Language

**Product**:
Ficha del catálogo que presenta una oferta con identidad, título y descripción. No implica que la ficha pueda comprarse directamente.
_Avoid_: artículo vendible, variante

**Simple product**:
Producto que representa una sola configuración comprable y no pertenece a un producto variable.
_Avoid_: variante simple

**Variable product**:
Producto que organiza opciones y variantes válidas. Su ficha genérica no se compra directamente.
_Avoid_: producto comprable, variante

**Product option**:
Dimensión que el comprador puede elegir, como `Processor` o `RAM`.
_Avoid_: variante, atributo

**Option value**:
Valor permitido para una opción, como `M5 Pro` o `24 GB`.
_Avoid_: opción, variante

**Product variant**:
Combinación explícita y comprable que selecciona exactamente un valor de cada opción de un producto variable.
_Avoid_: simple product, opción

**Sellable item**:
Unidad comercial concreta con identidad y precio que puede convertirse en una línea del carrito.
_Avoid_: product

**Bundle component**:
Artículo vendible atómico, simple product o product variant, que puede formar parte de un bundle.
_Avoid_: bundle, variable product

**Bundle**:
Oferta comprable con precio propio formada por al menos dos componentes. Un bundle no puede contener otro bundle.
_Avoid_: variable product, pack anidado

**Shopping cart**:
Selección mutable de artículos que el comprador considera comprar. Conserva referencias por identidad y cantidades; no fija precio ni garantiza stock.
_Avoid_: pedido, reserva de stock, checkout

**Cart line**:
Referencia a un único sellable item dentro del carrito junto con su cantidad positiva. Dos unidades del mismo artículo forman una línea con cantidad dos, no dos líneas.
_Avoid_: producto, snapshot, unidad de stock

**Checkout**:
Fase posterior al carrito en la que se vuelven a validar las condiciones de compra y pueden fijarse precio y disponibilidad durante un período limitado.
_Avoid_: shopping cart, pedido completado

**Cart pricer**:
Servicio de dominio sin estado que calcula el subtotal de un shopping cart cruzando la identidad de sus cart lines con los precios de un price book. Opera sobre objetos ya en memoria; nunca busca ni accede a datos.
_Avoid_: calculadora, PriceResolver, servicio de precios

**Price book**:
Mapa de precios por sellable item que el llamador resuelve y entrega completo al cart pricer. Puede cubrir más artículos que los del carrito. No es el catálogo ni una colección de productos.
_Avoid_: sellables, catálogo, lista de productos

**Cart pricing result**:
Resultado explícito de fijar precio al carrito: un subtotal (priced) o la lista completa de sellable items sin precio (failed). Nunca una excepción con datos ni una bandera de error.
_Avoid_: excepción de precios, resultado parcial
